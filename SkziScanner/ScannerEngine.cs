using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32;

namespace SkziScanner
{
    // Класс для сканирования директорий, хранилища сертификатов и сбора данных о СКЗИ
    public class ScannerEngine
    {
        // Основной метод сканирования списка компьютеров из формы
        public List<CertificateInfo> ScanComputers(List<string> targetComputers)
        {
            List<CertificateInfo> results = new List<CertificateInfo>();
            List<string> configDirs = GetDirectoriesFromConfig("config.txt");

            if (targetComputers == null || targetComputers.Count == 0)
            {
                targetComputers = new List<string> { Environment.MachineName };
            }

            foreach (string rawName in targetComputers)
            {
                string pcName = rawName.Trim();
                if (string.IsNullOrEmpty(pcName))
                {
                    continue;
                }

                Logger.LogInfo("Старт сканирования ПК: " + pcName);
                bool isLocal = IsLocalMachine(pcName);
                string cryptoVersion = GetCryptoProVersion(pcName, isLocal);

                // 1. Сканируем папки из конфига
                foreach (string dir in configDirs)
                {
                    try
                    {
                        string path = dir;
                        if (!isLocal && dir.Length >= 2 && dir[1] == ':')
                        {
                            path = @"\\" + pcName + @"\" + dir[0] + "$" + dir.Substring(2);
                        }

                        if (Directory.Exists(path))
                        {
                            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
                            foreach (string file in files)
                            {
                                string ext = Path.GetExtension(file).ToLower();
                                if (ext == ".cer" || ext == ".crt")
                                {
                                    try
                                    {
                                        X509Certificate2 cert = new X509Certificate2(file);
                                        results.Add(CreateCertInfo(cert, cryptoVersion));
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.LogError("Ошибка чтения файла " + file, ex);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("Ошибка доступа к папке " + dir + " на " + pcName, ex);
                    }
                }

                // 2. Сканируем личные сертификаты в хранилище Windows
                try
                {
                    X509Store store = isLocal
                        ? new X509Store(StoreName.My, StoreLocation.CurrentUser)
                        : new X509Store(@"\\" + pcName + @"\My", StoreLocation.LocalMachine);

                    store.Open(OpenFlags.ReadOnly);
                    foreach (X509Certificate2 cert in store.Certificates)
                    {
                        results.Add(CreateCertInfo(cert, cryptoVersion));
                    }
                    store.Close();
                }
                catch (Exception ex)
                {
                    Logger.LogError("Ошибка чтения хранилища сертификатов на " + pcName, ex);
                }
            }

            Logger.LogInfo("Сканирование завершено. Найдено сертификатов: " + results.Count);
            return results;
        }

        // Проверяет, относится ли адрес к текущему локальному компьютеру
        private bool IsLocalMachine(string pcName)
        {
            return pcName.Equals("localhost", StringComparison.OrdinalIgnoreCase)
                || pcName == "127.0.0.1"
                || pcName == "."
                || pcName.Equals(Environment.MachineName, StringComparison.OrdinalIgnoreCase);
        }

        // Читает список путей для сканирования из текстового конфига
        private List<string> GetDirectoriesFromConfig(string filePath)
        {
            List<string> dirs = new List<string>();
            try
            {
                if (!File.Exists(filePath))
                {
                    Logger.LogInfo("Файл конфигурации не найден: " + filePath);
                    return dirs;
                }

                foreach (string line in File.ReadAllLines(filePath))
                {
                    string cleanLine = line.Trim();
                    if (string.IsNullOrEmpty(cleanLine) || cleanLine.StartsWith("DB="))
                    {
                        continue;
                    }

                    if (cleanLine.StartsWith("DIR="))
                    {
                        dirs.Add(cleanLine.Substring(4).Trim());
                    }
                    else
                    {
                        dirs.Add(cleanLine);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Ошибка чтения конфига " + filePath, ex);
            }
            return dirs;
        }

        // Собирает нужные поля из сертификата в объект модели
        private CertificateInfo CreateCertInfo(X509Certificate2 cert, string cryptoVersion)
        {
            bool hasLicense = false;
            foreach (X509Extension ext in cert.Extensions)
            {
                if (ext.Oid != null && ext.Oid.Value == "1.2.643.2.2.49.2")
                {
                    hasLicense = true;
                    break;
                }
            }

            CertificateInfo info = new CertificateInfo();
            info.SerialNumber = cert.SerialNumber;
            info.OwnerName = cert.GetNameInfo(X509NameType.SimpleName, false);
            info.IssuerName = cert.GetNameInfo(X509NameType.SimpleName, true);
            info.CryptoProVersion = cryptoVersion;
            info.IsLicenseEmbedded = hasLicense;
            return info;
        }

        // Определяет версию установленного КриптоПро CSP через реестр ПК
        private string GetCryptoProVersion(string pcName, bool isLocal)
        {
            string regPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            RegistryView[] views = new RegistryView[] { RegistryView.Registry64, RegistryView.Registry32 };

            foreach (RegistryView view in views)
            {
                try
                {
                    RegistryKey baseKey = isLocal
                        ? RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, view)
                        : RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, pcName, view);

                    using (baseKey)
                    using (RegistryKey key = baseKey.OpenSubKey(regPath))
                    {
                        if (key == null)
                        {
                            continue;
                        }

                        foreach (string subName in key.GetSubKeyNames())
                        {
                            using (RegistryKey subKey = key.OpenSubKey(subName))
                            {
                                object name = subKey.GetValue("DisplayName");
                                if (name != null && name.ToString().Contains("CryptoPro CSP"))
                                {
                                    object ver = subKey.GetValue("DisplayVersion");
                                    return ver != null ? ver.ToString() : "Установлен";
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError("Ошибка проверки реестра на " + pcName, ex);
                }
            }

            return "Не установлен";
        }
    }
}
