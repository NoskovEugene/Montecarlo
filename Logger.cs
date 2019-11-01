using System;

namespace Montecarlomethod
{
    public class Logger
    {
        public void Error(string errorDescription){
            Console.Write(DateTime.Now);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"| Error: {errorDescription}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Warn(string warnDescription){
            Console.Write(DateTime.Now);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"| Warning: {warnDescription}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Info(string infoDescription){
            Console.Write(DateTime.Now);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"| Info: {infoDescription}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        

    }
}
