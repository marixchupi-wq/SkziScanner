using System;
using System.Runtime.InteropServices;
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

        private const int ATTACH_PARENT_PROCESS = -1;

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
                // Пытаемся подключиться к консоли родителя (если запущено из cmd)
                if (!AttachConsole(ATTACH_PARENT_PROCESS))
                {
                    // Родительской консоли нет — создаём свою
                    AllocConsole();
                }

                Console.WriteLine();
                Console.WriteLine("Консольный режим");
                RunConsole(args);
                Console.WriteLine("Нажмите Enter для выхода...");
                Console.ReadLine();
                FreeConsole();
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
            // Здесь код для консоли
        }

        static void RunGui(string[] args)
        {
            // Здесь код для окна (Application.Run(new MainForm()))
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
