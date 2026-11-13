using System;
using System.IO;

namespace SkziScanner
{
    // Класс для записи событий и исключений в файл логов
    public static class Logger
    {
        private static readonly string logFilePath = "app_logs.txt";
        private static readonly object lockObject = new object();

        // Записывает информационное сообщение в лог
        public static void LogInfo(string message)
        {
            WriteLog(message);
        }

        // Записывает информацию об ошибке в лог
        public static void LogError(string context, Exception ex)
        {
            string errorText = "ОШИБКА [" + context + "]: " + ex.Message;
            WriteLog(errorText);
        }

        // Формирует строку с датой и временем до миллисекунд и сохраняет в файл
        private static void WriteLog(string text)
        {
            try
            {
                lock (lockObject)
                {
                    string timeStamp = DateTime.Now.ToString("dd.MM.yy HH:mm:ss.fff");
                    string logLine = timeStamp + " - " + text + Environment.NewLine;
                    File.AppendAllText(logFilePath, logLine);
                }
            }
            catch
            {
                // Игнорируем ошибку записи лога, чтобы программа не завершилась аварийно
            }
        }
    }
}
