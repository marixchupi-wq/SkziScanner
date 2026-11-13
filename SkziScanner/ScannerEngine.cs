using System.Collections.Generic;

namespace SkziScanner
{
    // Класс для сканирования директорий на удаленных ПК и парсинга сертификатов
    public class ScannerEngine
    {
        // Сканирует указанные компьютеры и возвращает список найденных сертификатов
        public List<CertificateInfo> ScanComputers(List<string> targetComputers)
        {
            List<CertificateInfo> results = new List<CertificateInfo>();

            foreach (string pcName in targetComputers)
            {
                Logger.LogInfo("Начато сканирование ПК: " + pcName);

                // Временные тестовые данные (Разработчик 1 заменит этот блок на реальный парсинг)
                CertificateInfo testCert = new CertificateInfo();
                testCert.SerialNumber = "01D98F4A00B2" + pcName.GetHashCode().ToString("X");
                testCert.OwnerName = "Иванов Иван Иванович (" + pcName + ")";
                testCert.IssuerName = "УЦ ФГУП ГРЧЦ";
                testCert.CryptoProVersion = "5.0.12000";
                testCert.IsLicenseEmbedded = true;

                results.Add(testCert);
            }

            return results;
        }
    }
}
