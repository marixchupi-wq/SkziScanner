using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SkziScanner
{
    static class Program
    {
        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);

        [DllImport("kernel32.dll")]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        static extern bool FreeConsole();

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        private const int ATTACH_PARENT_PROCESS = -1;
        private const uint WM_CHAR = 0x0102;
        private const int VK_RETURN = 0x0D;

        // Главная точка входа для приложения
        [STAThread]
        static void Main(string[] args)
        {
            // Глобальный перехват всех необработанных исключений
            Application.ThreadException += (sender, arguments) => Logger.LogError("UI Ошибка", arguments.Exception);
            AppDomain.CurrentDomain.UnhandledException += (sender, arguments) => Logger.LogError("Критическая ошибка", arguments.ExceptionObject as Exception);

            Logger.LogInfo("Приложение запущено");

            bool consoleMode = args.Length > 0 && args[0] == "-console";

            if (consoleMode)
            {
                bool isAttached = AttachConsole(ATTACH_PARENT_PROCESS);
                if (!isAttached)
                {
                    AllocConsole();
                }

                Console.OutputEncoding = Encoding.UTF8;
                Console.InputEncoding = Encoding.UTF8;

                Console.WriteLine();
                Console.WriteLine("Консольный режим");
                RunConsole(args);

                if (isAttached)
                {
                    // Возвращаем приглашение командной строки (имитация нажатия Enter в cmd)
                    IntPtr cw = GetConsoleWindow();
                    if (cw != IntPtr.Zero)
                    {
                        PostMessage(cw, WM_CHAR, VK_RETURN, 0);
                    }
                }
                else
                {
                    Console.WriteLine("Нажмите Enter для выхода...");
                    Console.ReadLine();
                    FreeConsole();
                }
            }
            else
            {
                // Оконный режим
                RunGui(args);
            }

            Logger.LogInfo("Выход из приложения");
        }

        static void RunConsole(string[] args)
        {
            try
            {
                string pcInput = args.Skip(1).FirstOrDefault();
                string connString = args.Skip(2).FirstOrDefault();

                // Если параметры не переданы в команде запуска, запрашиваем их в этом же окне
                if (string.IsNullOrWhiteSpace(pcInput))
                {
                    Console.Write("Введите имена или IP компьютеров через запятую (Enter = localhost): ");
                    pcInput = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(pcInput))
                    {
                        pcInput = "localhost";
                    }

                    Console.Write("Введите строку подключения MSSQL (Enter = без записи в БД): ");
                    connString = Console.ReadLine();
                }

                List<string> targetComputers = new List<string>();
                string[] rawComputers = pcInput.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string rawPc in rawComputers)
                {
                    string trimmedPc = rawPc.Trim();
                    if (!string.IsNullOrEmpty(trimmedPc))
                    {
                        targetComputers.Add(trimmedPc);
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Запуск сканирования (" + string.Join(", ", targetComputers) + ")...");
                Logger.LogInfo("Консольный режим: запуск сканирования (" + targetComputers.Count + " ПК).");

                ScannerEngine engine = new ScannerEngine();
                List<CertificateInfo> certificates = engine.ScanComputers(targetComputers);

                Console.WriteLine("Найдено сертификатов: " + certificates.Count);
                Console.WriteLine(new string('-', 80));

                foreach (CertificateInfo cert in certificates)
                {
                    Console.WriteLine("Серийный номер:       " + cert.SerialNumber);
                    Console.WriteLine("Владелец:             " + cert.OwnerName);
                    Console.WriteLine("Удостоверяющий центр: " + cert.IssuerName);
                    Console.WriteLine("Версия КриптоПро:     " + cert.CryptoProVersion);
                    Console.WriteLine("Встроенная лицензия:  " + (cert.IsLicenseEmbedded ? "Да" : "Нет"));
                    Console.WriteLine(new string('-', 80));
                }

                if (!string.IsNullOrWhiteSpace(connString))
                {
                    Console.WriteLine("Сохранение результатов в базу данных...");
                    DatabaseHelper dbHelper = new DatabaseHelper(connString.Trim());
                    dbHelper.SaveCertificates(certificates);
                    Console.WriteLine("Данные успешно сохранены в MSSQL.");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Ошибка выполнения в консольном режиме", ex);
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        static void RunGui(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
