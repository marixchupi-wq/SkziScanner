using System.Collections.Generic;
using System.Data;

namespace SkziScanner
{
    // Класс для взаимодействия с базой данных MSSQL
    public class DatabaseHelper
    {
        private string connectionString;
        private static List<CertificateInfo> tempStorage = new List<CertificateInfo>();

        // Конструктор класса, принимающий строку подключения к БД
        public DatabaseHelper(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        // Проверяет наличие таблицы в БД, создает при отсутствии и сохраняет сертификаты
        public void SaveCertificates(List<CertificateInfo> certificates)
        {
            Logger.LogInfo("Сохранение сертификатов в БД (" + certificates.Count + " шт.)");

            // Временная имитация записи (Разработчик 3 заменит на реальный SQL-код)
            foreach (CertificateInfo cert in certificates)
            {
                tempStorage.RemoveAll(c => c.SerialNumber == cert.SerialNumber);
                tempStorage.Add(cert);
            }
        }

        // Получает все записи из БД для отображения в таблице интерфейса
        public DataTable GetAllCertificates()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Серийный номер", typeof(string));
            table.Columns.Add("Владелец", typeof(string));
            table.Columns.Add("Удостоверяющий центр", typeof(string));
            table.Columns.Add("Версия КриптоПро", typeof(string));
            table.Columns.Add("Встроенная лицензия", typeof(bool));

            foreach (CertificateInfo cert in tempStorage)
            {
                table.Rows.Add(cert.SerialNumber, cert.OwnerName, cert.IssuerName, cert.CryptoProVersion, cert.IsLicenseEmbedded);
            }

            return table;
        }
    }
}
