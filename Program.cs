using System.Collections.Generic;
using System;
using Analyzer;
using PolishNotation;
using Calculator;

namespace Montecarlomethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Expression >_");
            Montecarlo montecarlo = new Montecarlo(Console.ReadLine(),0,10);
            Console.ReadKey();
        }
    }
}
