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
            AddUiLog("Запуск сканирования (" + targetComputers.Count + " ПК)...");

            try
            {
                // Выполняем сетевое сканирование и запись в БД в фоновом потоке
                DataTable updatedTable = await Task.Run(() =>
                {
                    ScannerEngine scanner = new ScannerEngine();
                    List<CertificateInfo> foundCerts = scanner.ScanComputers(targetComputers);

                    DatabaseHelper dbHelper = new DatabaseHelper(currentConnectionString);
                    dbHelper.SaveCertificates(foundCerts);

                    return dbHelper.GetAllCertificates();
                });

                certificatesDataGridView.DataSource = updatedTable;
                AddUiLog("Сканирование успешно завершено. Таблица обновлена.");
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

            refreshButton.Enabled = false;
            AddUiLog("Запрос актуальных данных из БД...");

            try
            {
                DataTable table = await Task.Run(() =>
                {
                    DatabaseHelper dbHelper = new DatabaseHelper(currentConnectionString);
                    return dbHelper.GetAllCertificates();
                });

                certificatesDataGridView.DataSource = table;
                AddUiLog("Данные из БД загружены (строк: " + table.Rows.Count + ").");
            }
            catch (Exception ex)
            {
                Logger.LogError("Обновление таблицы из БД", ex);
                AddUiLog("ОШИБКА загрузки из БД: " + ex.Message);
            }
            finally
            {
                refreshButton.Enabled = true;
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
    }
}
