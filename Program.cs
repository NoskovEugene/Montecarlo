using System.Diagnostics.Contracts;
using System.ComponentModel;
using System.Net;
using System.Collections.Generic;
using System;
using Analyzer;
using PolishNotation;
using Calculator;
using AutoMapper;

namespace Montecarlomethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller controller = new Controller();
            controller.Start();
            // while(true)
            // {
            // Console.Write("Left: ");
            // if (int.TryParse(Console.ReadLine(), out int left))
            // {
            //     Console.Write("Right: ");
            //     if (int.TryParse(Console.ReadLine(), out int right))
            //     {
            //         Console.Write("Steps: ");
            //         if (int.TryParse(Console.ReadLine(), out int steps))
            //         {
            //             Console.Write("Expression >_");
            //             Montecarlo montecarlo = new Montecarlo(Console.ReadLine(), left, right);
            //             montecarlo.StartEmulate(steps);
            //             Console.ReadKey();
            //         }
            //     }
            // }
            // }
            Console.ReadKey();
        }
    }


    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap(typeof(string), typeof(int)).ConvertUsing(typeof(IntConverter));
            CreateMap(typeof(string),typeof(double)).ConvertUsing(typeof(DoubleConverter));
        }
    }

    public class DoubleConverter : ITypeConverter<string, double>
    {
        public double Convert(string source, double destination, ResolutionContext context)
        {
            return double .Parse(source);
        }
    }

    public class IntConverter : ITypeConverter<string, int>
    {
        public int Convert(string source, int destination, ResolutionContext context)
        {
            return int.Parse(source);
        }
    }
}
