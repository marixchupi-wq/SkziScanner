using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32;

namespace SkziScanner
{
    // Класс для сбора данных о сертификатах и СКЗИ напрямую из реестра Windows
    public class ScannerEngine
    {
        // Сканирует список компьютеров в локальной сети через редактор реестра
        public List<CertificateInfo> ScanComputers(List<string> targetComputers)
        {
            List<CertificateInfo> results = new List<CertificateInfo>();
            List<string> registryPaths = GetRegistryPathsFromConfig("config.txt");

            foreach (string pc in targetComputers)
            {
                try
                {
                    Logger.LogInfo("Подключение к реестру ПК: " + pc);
                    string cryptoProVer = GetCryptoProVersion(pc);

                    // Проверяем оба корневых раздела реестра: LocalMachine и CurrentUser
                    RegistryHive[] hives = new RegistryHive[] { RegistryHive.LocalMachine, RegistryHive.CurrentUser };
                    foreach (RegistryHive hive in hives)
                    {
                        using (RegistryKey baseKey = OpenTargetRegistry(pc, hive))
                        {
                            foreach (string regPath in registryPaths)
                            {
                                List<CertificateInfo> pcCerts = ReadCertificatesFromRegistry(baseKey, regPath, cryptoProVer);
                                results.AddRange(pcCerts);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError("Ошибка доступа к реестру ПК " + pc, ex);
                }
            }

            return results;
        }

        // Открывает нужный корневой раздел реестра локально или по сети
        private RegistryKey OpenTargetRegistry(string pcName, RegistryHive hive)
        {
            if (pcName.Equals("localhost", StringComparison.OrdinalIgnoreCase) || pcName == "127.0.0.1" || pcName.Equals(Environment.MachineName, StringComparison.OrdinalIgnoreCase))
            {
                return RegistryKey.OpenBaseKey(hive, RegistryView.Registry64);
            }

            return RegistryKey.OpenRemoteBaseKey(hive, pcName, RegistryView.Registry64);
        }

        // Читает список веток реестра для сканирования из конфигурационного файла
        private List<string> GetRegistryPathsFromConfig(string configPath)
        {
            List<string> paths = new List<string>();
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configPath);

            if (!File.Exists(fullPath))
            {
                Logger.LogInfo("Файл config.txt не найден, используется ветка по умолчанию.");
                paths.Add(@"SOFTWARE\Microsoft\SystemCertificates\My\Certificates");
                return paths;
            }

            string[] lines = File.ReadAllLines(fullPath);
            foreach (string line in lines)
            {
                string trimmed = line.Trim();
                if (!string.IsNullOrEmpty(trimmed) && !trimmed.StartsWith("#"))
                {
                    paths.Add(trimmed);
                }
            }

            return paths;
        }

        // Определяет версию установленного КриптоПро CSP из реестра
        private string GetCryptoProVersion(string pcName)
        {
            using (RegistryKey baseKey = OpenTargetRegistry(pcName, RegistryHive.LocalMachine))
            {
                string[] possiblePaths = new string[]
                {
                    @"SOFTWARE\Crypto Pro\Cryptography\CurrentVersion",
                    @"SOFTWARE\WOW6432Node\Crypto Pro\Cryptography\CurrentVersion"
                };

                foreach (string path in possiblePaths)
                {
                    using (RegistryKey key = baseKey.OpenSubKey(path))
                    {
                        if (key != null)
                        {
                            object versionObj = key.GetValue("Version");
                            if (versionObj != null)
                            {
                                return versionObj.ToString();
                            }
                        }
                    }
                }
            }

            return "Не установлен";
        }

        // Извлекает и парсит все сертификаты из указанной ветки реестра
        private List<CertificateInfo> ReadCertificatesFromRegistry(RegistryKey baseKey, string regPath, string cryptoProVersion)
        {
            List<CertificateInfo> list = new List<CertificateInfo>();

            using (RegistryKey certsRoot = baseKey.OpenSubKey(regPath))
            {
                if (certsRoot == null)
                {
                    return list;
                }

                string[] thumbprints = certsRoot.GetSubKeyNames();
                foreach (string thumbprint in thumbprints)
                {
                    try
                    {
                        using (RegistryKey certKey = certsRoot.OpenSubKey(thumbprint))
                        {
                            if (certKey == null)
                            {
                                continue;
                            }

                            byte[] blob = certKey.GetValue("Blob") as byte[];
                            if (blob == null)
                            {
                                continue;
                            }

                            X509Certificate2 cert = ParseCertificateFromBlob(blob);
                            if (cert != null)
                            {
                                CertificateInfo info = new CertificateInfo
                                {
                                    SerialNumber = cert.SerialNumber,
                                    OwnerName = cert.GetNameInfo(X509NameType.SimpleName, false),
                                    IssuerName = cert.GetNameInfo(X509NameType.SimpleName, true),
                                    CryptoProVersion = cryptoProVersion,
                                    IsLicenseEmbedded = CheckEmbeddedLicense(cert)
                                };

                                list.Add(info);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("Ошибка разбора сертификата в реестре (" + thumbprint + ")", ex);
                    }
                }
            }

            return list;
        }

        // Вырезает бинарное тело X.509 сертификата из структуры параметра Blob реестра Windows
        private X509Certificate2 ParseCertificateFromBlob(byte[] blob)
        {
            int offset = 0;
            while (offset + 12 <= blob.Length)
            {
                int propId = BitConverter.ToInt32(blob, offset);
                int dataLength = BitConverter.ToInt32(blob, offset + 8);
                offset = offset + 12;

                if (offset + dataLength > blob.Length || dataLength <= 0)
                {
                    break;
                }

                // Идентификатор 32 (0x20) соответствует самому телу сертификата CERT_CERT_PROP_ID
                if (propId == 32)
                {
                    byte[] rawCertData = new byte[dataLength];
                    Array.Copy(blob, offset, rawCertData, 0, dataLength);
                    return new X509Certificate2(rawCertData);
                }

                offset = offset + dataLength;
            }

            return null;
        }

        // Проверяет наличие встроенной лицензии КриптоПро по OID 1.2.643.2.2.49.2
        private bool CheckEmbeddedLicense(X509Certificate2 cert)
        {
            foreach (X509Extension ext in cert.Extensions)
            {
                if (ext.Oid != null && ext.Oid.Value.StartsWith("1.2.643.2.2.49"))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
