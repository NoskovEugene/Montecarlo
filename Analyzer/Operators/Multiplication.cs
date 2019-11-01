using Analyzer.Classes;
using System;

namespace Analyzer.Operators
{
    [Serializable]
    class Multiplication : ClassOperator
    {
        public override string Name { get; } = "*";

        public override int Priority => 3;

        public override double Invoke(double a, double b = 0)
        {
            return a * b;
        }
    }
}
