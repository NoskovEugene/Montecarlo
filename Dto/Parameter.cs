using System;
namespace Montecarlomethod.Dto
{
    public class Parameter
    {
        public string ParameterName{get;set;}

        public TypeParameter ParameterType{get;set;}

        public object ParameterValue{get;set;}
    }

    public class TypeParameter
    {
        public string NameType{get;set;}

        public Type Type{get;set;}
    }
}