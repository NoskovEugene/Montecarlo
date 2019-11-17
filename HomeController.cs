using System.IO.IsolatedStorage;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
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
using Montecarlomethod.Repositories;
using Montecarlomethod.Dto;

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
        
        public ParameterRepository Parameters {get;private set;}

        public Controller()
        {
            #region Create mapper
            MapperConfiguration config = new MapperConfiguration(cfg=> {
                cfg.AddProfile(new MapperProfile());
            });
            this.Mapper = config.CreateMapper();
            #endregion

            Parameters = new ParameterRepository(this.Mapper);
            var msg = this.Parameters.Add(new Parameter(){ParameterName = "left",ParameterValue = 0,ParameterType = new TypeParameter(){NameType = "double",Type = typeof(double)}});
            logger.ShowMessages(normalizer.Normalize(msg));
            msg = this.Parameters.Add(new Parameter(){ParameterName = "right",ParameterValue = 0,ParameterType = new TypeParameter(){NameType = "double",Type = typeof(double)}});
            logger.ShowMessages(normalizer.Normalize(msg));
            msg = this.Parameters.Add(new Parameter(){ParameterName = "expr",ParameterValue = 0,ParameterType = new TypeParameter(){NameType = "string",Type = typeof(string)}});
            logger.ShowMessages(normalizer.Normalize(msg));
            
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
            if(!string.IsNullOrEmpty(parameter))
            {
                return Parameters.GetValue(parameter);
            }
            else{
                string paramname = Query("Parameter name");
                return Parameters.GetValue(paramname);
            }
        }

        public Message Set(string parameter)
        {
            if(!string.IsNullOrEmpty(parameter))
            { 
                var tuple = NormalizeParameter(parameter);
                return Parameters.Set(tuple.Item1,tuple.Item2);
            }
            else{
                parameter = Query("Parameter");
                var tuple = NormalizeParameter(parameter);
                return Parameters.Set(tuple.Item1,tuple.Item2);
            }
        }
        
        #region set aux
        public Tuple<string,string> NormalizeParameter(string parameter)
        {
            string value = "";
            if(parameter.Contains('=')){
                string[] tmp = parameter.Split('=',StringSplitOptions.RemoveEmptyEntries);
                parameter = tmp[0];
                value = tmp [1];
                return new Tuple<string, string>(parameter,value);
            }
            else
            {
                value = Query("Value");
                return new Tuple<string, string>(parameter,value);
            }
        }
        
        #endregion


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
}

