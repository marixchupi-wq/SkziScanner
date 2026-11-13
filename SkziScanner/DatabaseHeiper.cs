using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SkziScanner
{
    // Класс для взаимодействия с базой данных MSSQL
    public class DatabaseHelper
    {
        private readonly string connectionString;

        // Конструктор класса, принимающий строку подключения к базе данных
        public DatabaseHelper(string dbConnectionString)
        {
            if (string.IsNullOrWhiteSpace(dbConnectionString))
            {
                throw new ArgumentException("Строка подключения к базе данных не заполнена.");
            }

            connectionString = dbConnectionString;
        }

        // Проверяет наличие таблицы в БД и создает её при отсутствии
        public void EnsureTableExists()
        {
            string createTableQuery = @"
				IF OBJECT_ID('dbo.Certificates', 'U') IS NULL
				BEGIN
					CREATE TABLE dbo.Certificates (
						SerialNumber VARCHAR(100) NOT NULL PRIMARY KEY,
						CreatedAt DATETIME NOT NULL,
						UpdatedAt DATETIME NOT NULL,
						OwnerName NVARCHAR(255) NULL,
						IssuerName NVARCHAR(255) NULL,
						CryptoProVersion NVARCHAR(100) NULL,
						IsLicenseEmbedded BIT NOT NULL
					);
				END";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // Сохраняет список сертификатов в БД, обновляя существующие записи без дублирования
        public void SaveCertificates(List<CertificateInfo> certificates)
        {
            EnsureTableExists();

            if (certificates == null || certificates.Count == 0)
            {
                Logger.LogInfo("Сертификаты для сохранения не найдены. Таблица проверена.");
                return;
            }

            string upsertQuery = @"
				IF EXISTS (SELECT 1 FROM dbo.Certificates WHERE SerialNumber = @SerialNumber)
				BEGIN
					UPDATE dbo.Certificates
					SET UpdatedAt = GETDATE(),
						OwnerName = @OwnerName,
						IssuerName = @IssuerName,
						CryptoProVersion = @CryptoProVersion,
						IsLicenseEmbedded = @IsLicenseEmbedded
					WHERE SerialNumber = @SerialNumber;
				END
				ELSE
				BEGIN
					INSERT INTO dbo.Certificates (
						SerialNumber,
						CreatedAt,
						UpdatedAt,
						OwnerName,
						IssuerName,
						CryptoProVersion,
						IsLicenseEmbedded
					)
					VALUES (
						@SerialNumber,
						GETDATE(),
						GETDATE(),
						@OwnerName,
						@IssuerName,
						@CryptoProVersion,
						@IsLicenseEmbedded
					);
				END";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (CertificateInfo cert in certificates)
                {
                    using (SqlCommand command = new SqlCommand(upsertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@SerialNumber", cert.SerialNumber ?? string.Empty);
                        command.Parameters.AddWithValue("@OwnerName", (object)cert.OwnerName ?? DBNull.Value);
                        command.Parameters.AddWithValue("@IssuerName", (object)cert.IssuerName ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CryptoProVersion", (object)cert.CryptoProVersion ?? DBNull.Value);
                        command.Parameters.AddWithValue("@IsLicenseEmbedded", cert.IsLicenseEmbedded);

                        command.ExecuteNonQuery();
                    }
                }
            }

            Logger.LogInfo("В базу данных успешно записано/обновлено записей: " + certificates.Count);
        }

        // Загружает все записи из таблицы сертификатов для отображения в интерфейсе
        public DataTable GetAllCertificates()
        {
            EnsureTableExists();

            DataTable resultTable = new DataTable();
            string selectQuery = @"
				SELECT 
					SerialNumber AS [Серийный номер],
					OwnerName AS [Владелец],
					IssuerName AS [Удостоверяющий центр],
					CryptoProVersion AS [Версия КриптоПро],
					IsLicenseEmbedded AS [Встроенная лицензия],
					CreatedAt AS [Дата внесения],
					UpdatedAt AS [Дата обновления]
				FROM dbo.Certificates
				ORDER BY UpdatedAt DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, connection))
                {
                    adapter.Fill(resultTable);
                }
            }

            return resultTable;
        }
    }
}
