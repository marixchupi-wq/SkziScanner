using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkziScanner
{
    // Главная форма панели управления модулем сбора данных о СКЗИ
    public partial class MainForm : System.Windows.Forms.Form
    {
        // Скрытая переменная для хранения строки подключения без вывода на экран
        private string currentConnectionString = string.Empty;

        // Конструктор главной формы приложения
        public MainForm()
        {
            InitializeComponent();

            this.ActiveControl = scanButton;

            AddUiLog("Панель управления инициализирована.");

            if (File.Exists("connection.settings"))
            {
                currentConnectionString = File.ReadAllText("connection.settings").Trim();
                CheckDatabaseConnection(currentConnectionString);
            }
            else
            {
                DbSettingsButton_Click(null, EventArgs.Empty);
            }
        }

        // Проверяет подключение к БД по сохраненной строке и выводит результат пользователю
        private void CheckDatabaseConnection(string connString)
        {
            try
            {
                using (SqlConnection testConn = new SqlConnection(connString))
                {
                    testConn.Open();
                }

                AddUiLog("Успешное подключение к базе данных.");
                MessageBox.Show("Подключение к базе данных успешно установлено!", "Подключение к БД", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Logger.LogError("Ошибка подключения к БД при запуске", ex);
                AddUiLog("ОШИБКА: Не удалось подключиться к базе данных (" + ex.Message + ").");
                MessageBox.Show("Не удалось подключиться к базе данных:\n" + ex.Message, "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обработчик нажатия кнопки запуска сканирования ПК
        private async void ScanButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(currentConnectionString))
            {
                MessageBox.Show("Сначала настройте подключение к базе данных.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DbSettingsButton_Click(null, EventArgs.Empty);
                return;
            }

            string rawPcs = targetPcsTextBox.Text.Trim();

            if (string.IsNullOrEmpty(rawPcs))
            {
                MessageBox.Show("Укажите хотя бы один компьютер для сканирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> targetComputers = rawPcs.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(pc => pc.Trim())
                .Where(pc => !string.IsNullOrEmpty(pc))
                .ToList();

            scanButton.Enabled = false;
            refreshButton.Enabled = false;
            editComputersButton.Enabled = false;
            dbSettingsButton.Enabled = false;
            AddUiLog("Запуск сканирования (" + targetComputers.Count + " ПК)...");

            try
            {
                List<CertificateInfo> foundCerts = new List<CertificateInfo>();

                // Выполняем сетевое сканирование и запись в БД в фоновом потоке
                DataTable updatedTable = await Task.Run(() =>
                {
                    ScannerEngine scanner = new ScannerEngine();
                    foundCerts = scanner.ScanComputers(targetComputers);

                    DatabaseHelper dbHelper = new DatabaseHelper(currentConnectionString);
                    dbHelper.SaveCertificates(foundCerts);

                    return dbHelper.GetAllCertificates();
                });

                DataTable combinedView = BuildCombinedCertificatesTable(foundCerts, updatedTable);
                certificatesDataGridView.Columns.Clear();
                certificatesDataGridView.DataSource = combinedView;
                HighlightSourceRows();

                AddUiLog("Найдено на ПК: " + foundCerts.Count + ", всего в БД: " + updatedTable.Rows.Count + ".");

                // Открываем диалог сохранения списка сертификатов в файл .cr
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Файл отчета сертификатов (*.cr)|*.cr";
                    saveDialog.FileName = "certificates_" + DateTime.Now.ToString("dd_MM_yyyy") + ".cr";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        SaveCertificatesToCrFile(foundCerts, saveDialog.FileName);
                        AddUiLog("Список сертификатов сохранен в файл: " + saveDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Процесс сканирования", ex);
                AddUiLog("ОШИБКА: " + ex.Message);
                MessageBox.Show("Произошла ошибка при сканировании: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                scanButton.Enabled = true;
                refreshButton.Enabled = true;
                editComputersButton.Enabled = true;
                dbSettingsButton.Enabled = true;
            }
        }

        // Обработчик нажатия кнопки ручного обновления таблицы из БД
        private async void RefreshButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(currentConnectionString))
            {
                MessageBox.Show("Сначала настройте подключение к базе данных.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            scanButton.Enabled = false;
            refreshButton.Enabled = false;
            editComputersButton.Enabled = false;
            dbSettingsButton.Enabled = false;
            AddUiLog("Запрос актуальных данных из БД...");

            try
            {
                DataTable table = await Task.Run(() =>
                {
                    DatabaseHelper dbHelper = new DatabaseHelper(currentConnectionString);
                    return dbHelper.GetAllCertificates();
                });

                DataTable combinedView = BuildCombinedCertificatesTable(null, table);
                certificatesDataGridView.Columns.Clear();
                certificatesDataGridView.DataSource = combinedView;
                HighlightSourceRows();

                AddUiLog("Данные из БД загружены (строк: " + table.Rows.Count + ").");
            }
            catch (Exception ex)
            {
                Logger.LogError("Обновление таблицы из БД", ex);
                AddUiLog("ОШИБКА загрузки из БД: " + ex.Message);
            }
            finally
            {
                scanButton.Enabled = true;
                refreshButton.Enabled = true;
                editComputersButton.Enabled = true;
                dbSettingsButton.Enabled = true;
            }
        }

        // Выводит сообщение с временем до миллисекунд в окно логов на форме и в файл
        private void AddUiLog(string message)
        {
            Logger.LogInfo(message);
            string timeStamp = DateTime.Now.ToString("dd.MM.yy HH:mm:ss.fff");
            logsTextBox.AppendText(timeStamp + " - " + message + Environment.NewLine);
            logsTextBox.SelectionStart = logsTextBox.Text.Length;
            logsTextBox.ScrollToCaret();
        }

        // Сохраняет список найденных сертификатов в файл с расширением .cr
        private void SaveCertificatesToCrFile(List<CertificateInfo> certificates, string filePath)
        {
            List<string> lines = new List<string>();
            lines.Add("Серийный номер;Владелец;Удостоверяющий центр;Версия КриптоПро;Встроенная лицензия;Дата выгрузки");

            string exportDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            foreach (CertificateInfo cert in certificates)
            {
                string licenseStatus = cert.IsLicenseEmbedded ? "Да" : "Нет";
                string line = string.Format("{0};{1};{2};{3};{4};{5}",
                    cert.SerialNumber,
                    cert.OwnerName,
                    cert.IssuerName,
                    cert.CryptoProVersion,
                    licenseStatus,
                    exportDate);

                lines.Add(line);
            }

            File.WriteAllLines(filePath, lines, System.Text.Encoding.UTF8);
        }

        // Объединяет сертификаты с ПК и из БД в одну таблицу с указанием источника
        private DataTable BuildCombinedCertificatesTable(List<CertificateInfo> pcCerts, DataTable dbTable)
        {
            DataTable combinedTable = new DataTable();
            combinedTable.Columns.Add("Источник данных", typeof(string));
            combinedTable.Columns.Add("Серийный номер", typeof(string));
            combinedTable.Columns.Add("Владелец", typeof(string));
            combinedTable.Columns.Add("Удостоверяющий центр", typeof(string));
            combinedTable.Columns.Add("Версия КриптоПро", typeof(string));
            combinedTable.Columns.Add("Встроенная лицензия", typeof(string));
            combinedTable.Columns.Add("Дата внесения", typeof(string));
            combinedTable.Columns.Add("Дата обновления", typeof(string));

            string currentTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");

            // 1. Сначала добавляем всё, что прямо сейчас нашлось на компьютерах
            if (pcCerts != null)
            {
                foreach (CertificateInfo cert in pcCerts)
                {
                    combinedTable.Rows.Add(
                        "Компьютер (Сканирование)",
                        cert.SerialNumber,
                        cert.OwnerName,
                        cert.IssuerName,
                        cert.CryptoProVersion,
                        cert.IsLicenseEmbedded ? "Да" : "Нет",
                        currentTime,
                        currentTime
                    );
                }
            }

            // 2. Затем добавляем записи из базы данных MSSQL по точному порядку столбцов таблицы Certificates
            if (dbTable != null)
            {
                foreach (DataRow row in dbTable.Rows)
                {
                    string serial = GetCellValue(row, new[] { "Серийный номер", "SerialNumber" }, 0);
                    string created = GetCellValue(row, new[] { "Дата внесения", "CreatedAt" }, 1);
                    string updated = GetCellValue(row, new[] { "Дата обновления", "UpdatedAt" }, 2);
                    string owner = GetCellValue(row, new[] { "Владелец", "OwnerName" }, 3);
                    string issuer = GetCellValue(row, new[] { "Удостоверяющий центр", "IssuerName" }, 4);
                    string version = GetCellValue(row, new[] { "Версия КриптоПро", "CryptoProVersion" }, 5);
                    string licenseRaw = GetCellValue(row, new[] { "Встроенная лицензия", "IsLicenseEmbedded" }, 6);

                    string license = (licenseRaw.Equals("True", StringComparison.OrdinalIgnoreCase) || licenseRaw == "1" || licenseRaw.Equals("Да", StringComparison.OrdinalIgnoreCase)) ? "Да" : "Нет";

                    combinedTable.Rows.Add(
                        "База данных (MSSQL)",
                        serial,
                        owner,
                        issuer,
                        version,
                        license,
                        created,
                        updated
                    );
                }
            }

            return combinedTable;
        }

        // Безопасно извлекает значение ячейки по списку возможных имен столбца или по индексу
        private string GetCellValue(DataRow row, string[] columnNames, int fallbackIndex)
        {
            foreach (string colName in columnNames)
            {
                if (row.Table.Columns.Contains(colName) && row[colName] != DBNull.Value)
                {
                    return row[colName].ToString();
                }
            }

            if (row.ItemArray.Length > fallbackIndex && row.ItemArray[fallbackIndex] != null)
            {
                return row.ItemArray[fallbackIndex].ToString();
            }

            return string.Empty;
        }

        // Открывает диалоговое окно настройки параметров подключения к MSSQL
        private void DbSettingsButton_Click(object sender, EventArgs e)
        {
            using (ConnectionForm connForm = new ConnectionForm())
            {
                if (connForm.ShowDialog() == DialogResult.OK)
                {
                    currentConnectionString = connForm.ConnectionString;
                    AddUiLog("Параметры подключения к БД успешно обновлены.");
                }
            }
        }

        // Открывает окно для удобного редактирования списка компьютеров
        private void EditComputersButton_Click(object sender, EventArgs e)
        {
            List<string> currentList = targetPcsTextBox.Text
                .Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(pc => pc.Trim())
                .Where(pc => !string.IsNullOrEmpty(pc))
                .ToList();

            using (ComputersForm pcsForm = new ComputersForm(currentList))
            {
                if (pcsForm.ShowDialog() == DialogResult.OK)
                {
                    targetPcsTextBox.Text = string.Join("; ", pcsForm.Addresses);
                    AddUiLog("Обновлен список целевых ПК (всего: " + pcsForm.Addresses.Count + ").");
                }
            }
        }

        // Подсвечивает строки, полученные напрямую с ПК, чтобы визуально отделить их от записей из БД
        private void HighlightSourceRows()
        {
            foreach (DataGridViewRow row in certificatesDataGridView.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                string source = row.Cells[0].Value?.ToString();
                if (source != null && source.StartsWith("Компьютер"))
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.AliceBlue;
                }
            }
        }
    }
}
