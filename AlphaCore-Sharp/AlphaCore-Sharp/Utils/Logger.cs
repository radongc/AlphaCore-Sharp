using AlphaCore_Sharp.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCore_Sharp.Utils
{
    public enum DebugLevel : int
    {
        NONE = 0x00,
        SUCCESS = 0x01,
        INFO = 0x02,
        ANTICHEAT = 0x04,
        WARNING = 0x08,
        ERROR = 0x10,
        DEBUG = 0x20,
    }

    internal static class Logger
    {
        private static bool ShouldLog(DebugLevel logType) => (Globals.LOGGING_MASK & (int)logType) == (int)logType;

        public static void Debug(string message)
        {
            if (ShouldLog(DebugLevel.DEBUG))
            {
                DateTime date = DateTime.Now;
                Console.WriteLine($"[DEBUG] [{date}] {message}");
            }
        }

        public static void Error(string message)
        {
            if (ShouldLog(DebugLevel.ERROR))
            {
                DateTime date = DateTime.Now;
                Console.WriteLine($"[ERROR] [{date}] {message}");
            }
        }

        public static void Warning(string message)
        {
            if (ShouldLog(DebugLevel.WARNING))
            {
                DateTime date = DateTime.Now;
                Console.WriteLine($"[WARNING] [{date}] {message}");
            }
        }

        public static void Anticheat(string message)
        {
            if (ShouldLog(DebugLevel.ANTICHEAT))
            {
                DateTime date = DateTime.Now;
                Console.WriteLine($"[ANTICHEAT] [{date}] {message}");
            }
        }

        public static void Info(string message)
        {
            if (ShouldLog(DebugLevel.INFO))
            {
                DateTime date = DateTime.Now;
                Console.WriteLine($"[INFO] [{date}] {message}");
            }
        }

        public static void Success(string message)
        {
            if (ShouldLog(DebugLevel.SUCCESS))
            {
                DateTime date = DateTime.Now;
                Console.WriteLine($"[SUCCESS] [{date}] {message}");
            }
        }
    }
}
