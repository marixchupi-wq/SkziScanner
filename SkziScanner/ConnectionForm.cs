using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace SkziScanner
{
    // Диалоговое окно настройки параметров подключения к серверу MSSQL
    public partial class ConnectionForm : Form
    {
        // Свойство для передачи готовой строки подключения в главную форму
        public string ConnectionString { get; private set; }

        // Конструктор окна подключения
        public ConnectionForm()
        {
            InitializeComponent();
            LoadSavedSettings();
        }

        // Загружает ранее сохраненные параметры из файла connection.settings, если он есть
        private void LoadSavedSettings()
        {
            try
            {
                if (File.Exists("connection.settings"))
                {
                    string savedConnStr = File.ReadAllText("connection.settings").Trim();
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(savedConnStr);

                    serverTextBox.Text = builder.DataSource;
                    databaseTextBox.Text = builder.InitialCatalog;
                    userTextBox.Text = builder.UserID;
                    passwordTextBox.Text = builder.Password;
                    trustCertificateCheckBox.Checked = builder.TrustServerCertificate;
                }
            }
            catch
            {
                // Если файл поврежден или пуст, оставляем поля для ручного ввода
            }
        }

        // Обработчик нажатия кнопки «Подключиться»
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(serverTextBox.Text) || string.IsNullOrWhiteSpace(databaseTextBox.Text))
            {
                MessageBox.Show("Укажите имя сервера и имя базы данных.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Собираем безопасную строку подключения из введенных полей
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = serverTextBox.Text.Trim();
            builder.InitialCatalog = databaseTextBox.Text.Trim();
            builder.UserID = userTextBox.Text.Trim();
            builder.Password = passwordTextBox.Text;
            builder.Encrypt = true;
            builder.TrustServerCertificate = trustCertificateCheckBox.Checked;
            builder.Pooling = true;
            builder.ConnectTimeout = 15;

            string generatedConnStr = builder.ConnectionString;
            connectButton.Enabled = false;

            try
            {
                // Проверяем реальное соединение с сервером перед сохранением
                using (SqlConnection testConn = new SqlConnection(generatedConnStr))
                {
                    testConn.Open();
                }

                File.WriteAllText("connection.settings", generatedConnStr);
                ConnectionString = generatedConnStr;

                Logger.LogInfo("Успешно настроено подключение к БД: " + builder.DataSource);
                MessageBox.Show("Подключение к базе данных успешно установлено!", "Подключение к БД", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Logger.LogError("Ошибка проверки подключения к БД", ex);
                MessageBox.Show("Не удалось подключиться к серверу:\n" + ex.Message, "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectButton.Enabled = true;
            }
        }

        // Обработчик нажатия кнопки «Отмена»
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
