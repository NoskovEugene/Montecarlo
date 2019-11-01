using Analyzer.Classes;
using System;

namespace Analyzer.Operators
{
    [Serializable]
    class Addition : ClassOperator
    {
        public override string Name { get; } = "+";

        public override int Priority => 2;

        public override double Invoke(double a, double b = 0)
        {
            return a + b;
        }
    }
}
