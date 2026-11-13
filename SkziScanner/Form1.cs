using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkziScanner
{
    // Главная форма панели управления модулем сбора данных о СКЗИ
    public partial class Form1 : Form
    {
        // Конструктор главной формы приложения
        public Form1()
        {
            InitializeComponent();
            AddUiLog("Панель управления инициализирована.");
        }

        // Обработчик нажатия кнопки запуска сканирования ПК
        private async void btnScan_Click(object sender, EventArgs e)
        {
            string rawPcs = txtTargetPcs.Text.Trim();
            string connString = txtConnectionString.Text.Trim();

            if (string.IsNullOrEmpty(rawPcs))
            {
                MessageBox.Show("Укажите хотя бы один компьютер для сканирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> targetComputers = rawPcs.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(pc => pc.Trim())
                .Where(pc => !string.IsNullOrEmpty(pc))
                .ToList();

            btnScan.Enabled = false;
            btnRefresh.Enabled = false;
            AddUiLog("Запуск сканирования (" + targetComputers.Count + " ПК)...");

            try
            {
                // Выполняем сетевое сканирование и запись в БД в фоновом потоке
                DataTable updatedTable = await Task.Run(() =>
                {
                    ScannerEngine scanner = new ScannerEngine();
                    List<CertificateInfo> foundCerts = scanner.ScanComputers(targetComputers);

                    DatabaseHelper dbHelper = new DatabaseHelper(connString);
                    dbHelper.SaveCertificates(foundCerts);

                    return dbHelper.GetAllCertificates();
                });

                dgvCertificates.DataSource = updatedTable;
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
                btnScan.Enabled = true;
                btnRefresh.Enabled = true;
            }
        }

        // Обработчик нажатия кнопки ручного обновления таблицы из БД
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            string connString = txtConnectionString.Text.Trim();
            btnRefresh.Enabled = false;
            AddUiLog("Запрос актуальных данных из БД...");

            try
            {
                DataTable table = await Task.Run(() =>
                {
                    DatabaseHelper dbHelper = new DatabaseHelper(connString);
                    return dbHelper.GetAllCertificates();
                });

                dgvCertificates.DataSource = table;
                AddUiLog("Данные из БД загружены (строк: " + table.Rows.Count + ").");
            }
            catch (Exception ex)
            {
                Logger.LogError("Обновление таблицы из БД", ex);
                AddUiLog("ОШИБКА загрузки из БД: " + ex.Message);
            }
            finally
            {
                btnRefresh.Enabled = true;
            }
        }

        // Выводит сообщение с временем до миллисекунд в окно логов на форме и в файл
        private void AddUiLog(string message)
        {
            Logger.LogInfo(message);
            string timeStamp = DateTime.Now.ToString("dd.MM.yy HH:mm:ss.fff");
            txtLogs.AppendText(timeStamp + " - " + message + Environment.NewLine);
        }
    }
}
