using System;
using System.Collections.Generic;
namespace Montecarlomethod
{
    public class MessageNormalizer
    {
        public List<Message> Normalize(Message message)
        {
            List<Message> messages = new List<Message>();
            if(message.Value.Contains("\r\n")){
                var values = message.Value.Split(new string[] {"\r\n"},StringSplitOptions.RemoveEmptyEntries);
                foreach(var item in values){
                    messages.Add(new Message(message.Type,item));
                }
            }
            else{
                messages.Add(message);
            }
            return messages;
        }
    }
}