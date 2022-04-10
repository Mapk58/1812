using System;
using System.Collections.Generic;
using System.Text;

namespace _1812.Domain
{
    class LogManager //debug, info, warn, error
    {
        private static int level = 0;
        public static int Level { get { return level; } set { if ((level >= 0) && (level <= 4)) { level = value; } } }

        private static int DebugCount = 1;
        private static int InfoCount = 1;
        private static int WarningCount = 1;
        private static int ErrorCount = 1;
        private static void Publicate(string message)
        {
            Console.WriteLine(message);
        }
        public static void Debug(string message)
        {
            if (Level >= 4)
            {
                Publicate("DBUG[" + DebugCount + "] " + message);
            }
            DebugCount++;
        }
        public static void Info(string message)
        {
            if (Level >= 3)
            {
                Publicate("INFO[" + InfoCount + "] " + message);
            }
            InfoCount++;
        }
        public static void Warning(string message)
        {
            if (Level >= 2)
            {
                Publicate("WARN[" + WarningCount + "] " + message);
            }
            WarningCount++;
        }
        public static void Error(string message)
        {
            if (Level >= 1)
            {
                Publicate("FATA[" + ErrorCount + "] " + message);
            }
            ErrorCount++;
        }
    }
}
