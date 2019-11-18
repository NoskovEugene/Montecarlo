using System.Linq;
using System.Collections.Generic;
using Montecarlomethod.Dto;
using System;
using AutoMapper;

namespace Montecarlomethod.Repositories
{
    public class ParameterRepository
    {
        public Dictionary<string,Parameter> Dict {get;private set;}

        public IMapper mapper { get; }

        public ParameterRepository(IMapper mapper)
        {
            Dict = new Dictionary<string, Parameter>();
            this.mapper = mapper;
        }

        public Parameter Get(string Name)
        {
            if(Dict.ContainsKey(Name)){
                return Dict[Name];
            }else{
                return null;
            }
        }

        public Message GetValue(string Name){
            try{
                Parameter target = Get(Name);
                return new Message(TypeMessage.Message,$"{Name} = {target.ParameterValue}");
            } catch(Exception ex){
                return new Message(TypeMessage.Error,ex.Message);
            }
        }

        public Message Set(string Name, string Value)
        {
            if(Dict.ContainsKey(Name)){
                Parameter target = Dict[Name];
                try{
                    Dict[Name].ParameterValue = mapper.Map(Value,Value.GetType(),target.ParameterType.Type);
                    return new Message(TypeMessage.Info,"Parameter changed");
                }
                catch(Exception ex){
                    return new Message(TypeMessage.Error,$"Unknown error while mapping type\r\nParameter not changed\r\nException code: {ex.Message}");
                }
            }
            else{
                return new Message(TypeMessage.Error,"Parameter not found");
            }
        }

        public Message Add(Parameter parameter){
            if(Dict.ContainsValue(parameter)){
                return new Message(TypeMessage.Error,"parameter contains");
            }
            else
            {
                Dict.Add(parameter.ParameterName,parameter);
                return new Message(TypeMessage.Info,$"Parameter {parameter.ParameterName} added");
            }
        }

    }
}