using System.Net;
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
            while(true){
            Console.Write("Left: ");
            if (int.TryParse(Console.ReadLine(), out int left))
            {
                Console.Write("Right: ");
                if (int.TryParse(Console.ReadLine(), out int right))
                {
                    Console.Write("Steps: ");
                    if (int.TryParse(Console.ReadLine(), out int steps))
                    {
                        Console.Write("Expression >_");
                        Montecarlo montecarlo = new Montecarlo(Console.ReadLine(), left, right);
                        montecarlo.StartEmulate(steps);
                        Console.ReadKey();
                    }
                }
            }
            }
        }
    }
}
