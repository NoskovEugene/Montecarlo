using System.Linq;
using System.Reflection;
using System.Numerics;
using System.Threading;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System;
using AutoMapper;
namespace Montecarlomethod
{
    public class Controller
    {
        public Logger logger = new Logger();

        public MessageNormalizer normalizer = new MessageNormalizer();
        public IMapper Mapper {get;set;}
        public string ConsoleParameter {get;set;}
        public delegate Message CommandDelegate(string parameter);
        public Dictionary<string,CommandDelegate> Commands {get;private set;} = new Dictionary<string, CommandDelegate>();
        public Dictionary<string,Parameter> Parameters = new Dictionary<string, Parameter>()
        {
            {"expr",new Parameter("Expression",typeof(string),"")},
            {"left", new Parameter("Left",typeof(int),-1)},
            {"right",new Parameter("right",typeof(int),-1)}
        };

        public Controller()
        {
            #region Create mapper
            MapperConfiguration config = new MapperConfiguration(cfg=> {
                cfg.AddProfile(new MapperProfile());
            });
            this.Mapper = config.CreateMapper();
            #endregion


            Commands.Add("set",(parameter)=>Set(parameter));
            Commands.Add("get",(parameter)=>Get(parameter));
        }

        public void Start()
        {
            while(true){
                Console.Write($"{this.ConsoleParameter}>_ ");
                var input = Console.ReadLine();
                var arr = input.Split(new string[] {" "},StringSplitOptions.RemoveEmptyEntries);
                if(Commands.ContainsKey(arr[0])){
                    Message msg = Commands[arr[0]](string.Join(" ",arr.Where(x=> x!=arr[0]).Select(x=>x)));
                    var msgs = normalizer.Normalize(msg);
                    logger.ShowMessages(msgs);
                }
            }
        }

        public Message Get(string parameter)
        {
            if(!string.IsNullOrEmpty(parameter)){}
            else{
                parameter = this.Query("parameter");
                if(Parameters.ContainsKey(parameter)){
                    return new Message(TypeMessage.Message,Parameters[parameter].ParameterValue.ToString());
                }
                else{
                    return new Message(TypeMessage.Error,"Parameter was not found");
                }
            }
            return null;
        }

        public Message Set(string parameter)
        {
            if(!string.IsNullOrEmpty(parameter))
            { 

                return new Message(TypeMessage.Message,"");
            }
            else{
                parameter = this.Query("parameter");
                string value = "";
                if(!parameter.Contains('=')){
                    value = this.Query("value");             
                }
                else{
                    string[] tmpArr = parameter.Split(new char[] { '='});
                    parameter = tmpArr[0];
                    value = tmpArr[1];
                }
                if(Parameters.ContainsKey(parameter))
                {
                    Type paramType = Parameters[parameter].ParameterType;
                    try
                    {
                        Parameter target = Parameters[parameter];
                        Parameters[parameter].ParameterValue = Mapper.Map(value,value.GetType(),target.ParameterType);
                    }
                    catch (System.Exception ex)
                    {
                        return new Message(TypeMessage.Error,$"Unknown error while mapping type\r\n{ex.Message}");
                    }
                    return new Message(TypeMessage.Info,$"Parameter changed\r\nNow {Parameters[parameter].ParameterName}={Parameters[parameter].ParameterValue}");
                }
                else{
                    return new Message(TypeMessage.Error, "Parameter not found");
                }
            }
        }

        private string Query(string text = "")
        {
            if(!string.IsNullOrEmpty(text)){
                Console.Write($"{text} >_ ");
                return Console.ReadLine();
            }else{
                Console.Write($"{this.ConsoleParameter} >_ ");
                return Console.ReadLine();
            }
        }
    }





    public class Message{
        public TypeMessage Type{get;set;}
        public string Value {get;set;}

        public Message(TypeMessage type, string value)
        {
            this.Type = type;
            this.Value = value;
        }
    }

    public enum TypeMessage
    {
        Error,
        Info,
        Message
    }

    public class Parameter
    {
        public string ParameterName{get;set;}

        public Type ParameterType{get;set;}

        public object ParameterValue{get;set;}

        public Parameter(string name,Type type,object value)
        {
            this.ParameterName = name;
            this.ParameterType = type;
            this.ParameterValue = value;
        }
    }
}

