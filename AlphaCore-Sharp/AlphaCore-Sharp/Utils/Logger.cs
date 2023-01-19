using AlphaCore_Sharp.Game;

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
        // Bitwise AND between logType and logging mask; if logType is included in the mask, it will return the value of logType. If not, it returns 0.
        private static bool ShouldLog(DebugLevel logType) => (Globals.Logging.LOGGING_MASK & (int)logType) == (int)logType;

        public static void Debug(string message)
        {
            if (ShouldLog(DebugLevel.DEBUG))
            {
                DateTime date = DateTime.Now;

                ConsoleColor oldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine($"[DEBUG] [{date}] {message}");

                Console.ForegroundColor = oldColor;
            }
        }

        public static void Error(string message)
        {
            if (ShouldLog(DebugLevel.ERROR))
            {
                DateTime date = DateTime.Now;

                ConsoleColor oldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine($"[ERROR] [{date}] {message}");

                Console.ForegroundColor = oldColor;
            }
        }

        public static void Warning(string message)
        {
            if (ShouldLog(DebugLevel.WARNING))
            {
                DateTime date = DateTime.Now;

                ConsoleColor oldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine($"[WARNING] [{date}] {message}");

                Console.ForegroundColor = oldColor;
            }
        }

        public static void Anticheat(string message)
        {
            if (ShouldLog(DebugLevel.ANTICHEAT))
            {
                DateTime date = DateTime.Now;

                ConsoleColor oldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkYellow;

                Console.WriteLine($"[ANTICHEAT] [{date}] {message}");

                Console.ForegroundColor = oldColor;
            }
        }

        public static void Info(string message)
        {
            if (ShouldLog(DebugLevel.INFO))
            {
                DateTime date = DateTime.Now;

                ConsoleColor oldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Blue;

                Console.WriteLine($"[INFO] [{date}] {message}", Console.ForegroundColor);

                Console.ForegroundColor = oldColor;
            }
        }

        public static void Success(string message)
        {
            if (ShouldLog(DebugLevel.SUCCESS))
            {
                DateTime date = DateTime.Now;
                
                ConsoleColor oldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine($"[SUCCESS] [{date}] {message}", Console.ForegroundColor);

                Console.ForegroundColor = oldColor;
            }
        }

        //** Other Methods **//

        public static void Message(string message)
        {
            Console.WriteLine(message);
        }

        public static void NewLine()
        {
            Console.WriteLine();
        }

        public static void PrintLogSettings()
        {
            Message("Currently logging: ");
            if (ShouldLog(DebugLevel.SUCCESS))
                Message("   SUCCESS");
            if (ShouldLog(DebugLevel.INFO))
                Message("   INFO");
            if (ShouldLog(DebugLevel.ANTICHEAT))
                Message("   ANTICHEAT");
            if (ShouldLog(DebugLevel.WARNING))
                Message("   WARNING");
            if (ShouldLog(DebugLevel.ERROR))
                Message("   ERROR");
            if (ShouldLog(DebugLevel.DEBUG))
                Message("   DEBUG");
            NewLine();
        }
    }
}
