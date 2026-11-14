using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkziScanner
{
    // Диалоговое окно для удобного редактирования списка целевых компьютеров
    public partial class ComputersForm : Form
    {
        // Список введенных и очищенных адресов компьютеров
        public List<string> Addresses { get; private set; }

        // Конструктор окна списка ПК
        public ComputersForm(List<string> existingAddresses)
        {
            InitializeComponent();

            // Запрещаем менять высоту строк и ширину столбцов мышкой
            computersDataGridView.AllowUserToResizeRows = false;
            computersDataGridView.AllowUserToResizeColumns = false;
            computersDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            computersDataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            // Включаем удобное выделение целой строки при клике (с зажатым Ctrl или Shift можно выбрать несколько)
            computersDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            computersDataGridView.MultiSelect = true;

            Addresses = new List<string>();

            // Если в таблице еще не созданы столбцы в дизайнере — создаем столбец программно
            if (computersDataGridView.Columns.Count == 0)
            {
                computersDataGridView.Columns.Add("AddressColumn", "Имя ПК или IP-адрес");
                computersDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            // Заполняем таблицу уже сохраненными адресами, если они были
            if (existingAddresses != null)
            {
                foreach (string pc in existingAddresses)
                {
                    computersDataGridView.Rows.Add(pc);
                }
            }
        }

        // Собирает только выделенные адреса из таблицы и закрывает окно
        private void SaveButton_Click(object sender, EventArgs e)
        {
            Addresses.Clear();

            foreach (DataGridViewRow row in computersDataGridView.SelectedRows)
            {
                // Пропускаем новую пустую строку внизу таблицы
                if (row.IsNewRow)
                {
                    continue;
                }

                string value = row.Cells[0].Value?.ToString().Trim();
                if (!string.IsNullOrEmpty(value) && !Addresses.Contains(value))
                {
                    Addresses.Add(value);
                }
            }

            // Если ни одна строка не выделена — предупреждаем пользователя
            if (Addresses.Count == 0)
            {
                MessageBox.Show("Выделите в таблице хотя бы один компьютер для добавления (можно выбрать несколько, зажав Ctrl).", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Ищет активные компьютеры в локальной сети (и Radmin VPN) через параллельный Ping
        private async void ScanNetworkButton_Click(object sender, EventArgs e)
        {
            scanNetworkButton.Enabled = false;
            string originalText = scanNetworkButton.Text;
            scanNetworkButton.Text = "Поиск...";

            try
            {
                List<string> foundIps = await DiscoverLocalNetworkHostsAsync();
                int addedCount = 0;

                foreach (string ip in foundIps)
                {
                    if (!IsAddressAlreadyInGrid(ip))
                    {
                        computersDataGridView.Rows.Add(ip);
                        addedCount++;
                    }
                }

                MessageBox.Show("Поиск завершен. Новых устройств добавлено: " + addedCount, "Поиск в сети", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Logger.LogError("Ошибка поиска устройств в локальной сети", ex);
                MessageBox.Show("Ошибка при сканировании сети: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                scanNetworkButton.Text = originalText;
                scanNetworkButton.Enabled = true;
            }
        }

        // Проверяет, есть ли уже указанный IP-адрес в таблице
        private bool IsAddressAlreadyInGrid(string address)
        {
            foreach (DataGridViewRow row in computersDataGridView.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                string existing = row.Cells[0].Value?.ToString().Trim();
                if (string.Equals(existing, address, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        // Определяет активные сетевые адаптеры и параллельно опрашивает подсети
        private async Task<List<string>> DiscoverLocalNetworkHostsAsync()
        {
            List<string> activeHosts = new List<string>();
            List<string> subnets = GetLocalSubnets();
            List<Task<string>> pingTasks = new List<Task<string>>();

            foreach (string subnet in subnets)
            {
                for (int i = 1; i < 255; i++)
                {
                    string targetIp = subnet + "." + i;
                    pingTasks.Add(PingHostAsync(targetIp));
                }
            }

            string[] results = await Task.WhenAll(pingTasks);
            foreach (string res in results)
            {
                if (!string.IsNullOrEmpty(res) && !activeHosts.Contains(res))
                {
                    activeHosts.Add(res);
                }
            }

            return activeHosts;
        }

        // Получает префиксы подсетей только для реальной локальной сети и Radmin VPN
        private List<string> GetLocalSubnets()
        {
            List<string> prefixes = new List<string>();

            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.OperationalStatus != OperationalStatus.Up || ni.NetworkInterfaceType == NetworkInterfaceType.Loopback)
                {
                    continue;
                }

                foreach (UnicastIPAddressInformation ipInfo in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ipInfo.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        string ipStr = ipInfo.Address.ToString();

                        // Оставляем только реальную локальную сеть (192.168.*, 10.*) и Radmin VPN (26.*)
                        if (!ipStr.StartsWith("192.168.") && !ipStr.StartsWith("10.") && !ipStr.StartsWith("26."))
                        {
                            continue;
                        }

                        int lastDot = ipStr.LastIndexOf('.');
                        if (lastDot > 0)
                        {
                            string prefix = ipStr.Substring(0, lastDot);
                            if (!prefixes.Contains(prefix))
                            {
                                prefixes.Add(prefix);
                            }
                        }
                    }
                }
            }

            return prefixes;
        }


        // Проверяет, действительно ли на узле открыт сетевой порт Windows (445/SMB), отсекая фантомные адреса VPN
        private async Task<string> PingHostAsync(string ipAddress)
        {
            try
            {
                using (TcpClient tcpClient = new TcpClient())
                {
                    Task connectTask = tcpClient.ConnectAsync(ipAddress, 445);
                    Task timeoutTask = Task.Delay(150);

                    Task completedTask = await Task.WhenAny(connectTask, timeoutTask);
                    if (completedTask == connectTask && tcpClient.Connected)
                    {
                        return ipAddress;
                    }
                }
            }
            catch
            {
                // Игнорируем закрытые порты и недоступные узлы
            }

            return null;
        }
    }
}
