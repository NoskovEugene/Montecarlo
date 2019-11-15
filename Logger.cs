using System.Collections.Generic;
using System;

namespace Montecarlomethod
{
    public class Logger
    {
        
        public delegate void ShowMethod(string parameter);

        public Dictionary<TypeMessage, ShowMethod> dict = new Dictionary<TypeMessage, ShowMethod>();

        public Logger()
        {
            dict.Add(TypeMessage.Error,(param)=>Error(param));
            dict.Add(TypeMessage.Info,(param)=>Info(param));
            dict.Add(TypeMessage.Message,(param)=>Message(param));
        }

        private void Message(string messageDescription)
        {
            Console.Write(DateTime.Now);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"| Message: {messageDescription}");
            Console.ForegroundColor = ConsoleColor.White;
        }

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

        public void ShowMessage(Message message)
        {
            if(dict.ContainsKey(message.Type))
            {
                dict[message.Type](message.Value);
            }
        }
        public void ShowMessages(List<Message> messages){
            foreach(var item in messages){
                ShowMessage(item);
            }
        }
    }
}
