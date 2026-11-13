using System;
using System.Windows.Forms;

namespace SkziScanner
{
    static class Program
    {
        // Главная точка входа для приложения
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Глобальный перехват всех необработанных исключений
            Application.ThreadException += (sender, args) => Logger.LogError("UI Ошибка", args.Exception);
            AppDomain.CurrentDomain.UnhandledException += (sender, args) => Logger.LogError("Критическая ошибка", args.ExceptionObject as Exception);

            Logger.LogInfo("Приложение запущено");
            Application.Run(new Form());
            Logger.LogInfo("Выход из приложения");
        }
    }
}
