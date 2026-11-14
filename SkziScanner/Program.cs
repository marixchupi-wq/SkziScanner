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
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_HIDE = 0;

        [STAThread]
        static void Main(string[] args)
        {
            Application.ThreadException += (sender, arguments) => Logger.LogError("UI Ошибка", arguments.Exception);
            AppDomain.CurrentDomain.UnhandledException += (sender, arguments) => Logger.LogError("Критическая ошибка", arguments.ExceptionObject as Exception);

            Logger.LogInfo("Приложение запущено");

            bool consoleMode = args.Length > 0 && args[0] == "-console";

            if (consoleMode)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine("Консольный режим");
                RunConsole(args);
            }
            else
            {
                // Скрываем окно консоли при запуске графического интерфейса
                IntPtr handle = GetConsoleWindow();
                if (handle != IntPtr.Zero)
                {
                    ShowWindow(handle, SW_HIDE);
                }

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
