using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;

namespace SkziScanner
{
    // Класс для взаимодействия с базой данных MSSQL
    public class DatabaseHelper
    {
        private readonly string connectionString;

        // Конструктор класса, принимающий и настраивающий строку подключения к базе данных
        public DatabaseHelper(string dbConnectionString)
        {
            if (string.IsNullOrWhiteSpace(dbConnectionString))
            {
                throw new ArgumentException("Строка подключения к базе данных не заполнена.");
            }

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(dbConnectionString);
            builder.Pooling = false;
            builder.Encrypt = false;
            if (builder.ConnectTimeout < 45)
            {
                builder.ConnectTimeout = 45;
            }

            connectionString = builder.ConnectionString;
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
						CryptoProVersion NVARCHAR(200) NULL,
						IsLicenseEmbedded BIT NOT NULL
					);
				END";

            ExecuteWithRetry(() =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(createTableQuery, connection))
                    {
                        command.CommandTimeout = 60;
                        command.ExecuteNonQuery();
                    }
                }
            });
        }

        // Сохраняет список сертификатов в БД, обновляя существующие записи без дублирования
        public void SaveCertificates(List<CertificateInfo> certificates)
        {
            try
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

                foreach (CertificateInfo cert in certificates)
                {
                    ExecuteWithRetry(() =>
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            using (SqlCommand command = new SqlCommand(upsertQuery, connection))
                            {
                                command.CommandTimeout = 60;
                                command.Parameters.AddWithValue("@SerialNumber", cert.SerialNumber ?? string.Empty);
                                command.Parameters.AddWithValue("@OwnerName", (object)cert.OwnerName ?? DBNull.Value);
                                command.Parameters.AddWithValue("@IssuerName", (object)cert.IssuerName ?? DBNull.Value);
                                command.Parameters.AddWithValue("@CryptoProVersion", (object)cert.CryptoProVersion ?? DBNull.Value);
                                command.Parameters.AddWithValue("@IsLicenseEmbedded", cert.IsLicenseEmbedded);

                                command.ExecuteNonQuery();
                            }
                        }
                    });
                }

                Logger.LogInfo("В базу данных успешно записано/обновлено записей: " + certificates.Count);
            }
            catch (Exception ex)
            {
                Logger.LogError("Ошибка при сохранении данных в MSSQL", ex);
                MessageBox.Show(
                    "Не удалось сохранить данные в БД: " + ex.Message,
                    "Ошибка подключения к MSSQL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Выполняет операцию с БД с переподключением при сетевом сбое
        private void ExecuteWithRetry(Action action)
        {
            int maxRetries = 4;
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    action();
                    return;
                }
                catch (SqlException)
                {
                    if (attempt == maxRetries)
                    {
                        throw;
                    }

                    Thread.Sleep(1500);
                }
            }
        }

        // Загружает все записи из таблицы сертификатов для отображения в интерфейсе
        public DataTable GetAllCertificates()
        {
            DataTable resultTable = new DataTable();

            try
            {
                EnsureTableExists();

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

                ExecuteWithRetry(() =>
                {
                    resultTable.Clear();
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, connection))
                        {
                            adapter.SelectCommand.CommandTimeout = 60;
                            adapter.Fill(resultTable);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Logger.LogError("Ошибка при загрузке данных из MSSQL", ex);
            }

            return resultTable;
        }
    }
}
