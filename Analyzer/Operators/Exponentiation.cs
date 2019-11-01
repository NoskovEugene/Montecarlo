using System;
using Analyzer.Classes;

namespace Analyzer.Operators
{
    [Serializable]
    class Exponentiation : ClassOperator
    {
        public override string Name => "^";

        public override int Priority => 3;

        public override double Invoke(double a, double b = 0)
        {
            return Math.Pow(a, b);
        }
    }
}
