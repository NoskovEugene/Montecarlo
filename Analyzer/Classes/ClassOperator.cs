using System;
using System.Collections.Generic;

namespace Analyzer.Classes
{
    /// <summary>
    /// Operator element
    /// </summary>
    [Serializable]
    abstract class ClassOperator : IElement
    {
        public abstract string Name { get; }

        public double Value { get; } = 0;

        public ElementType Type { get; } = ElementType.Operator;

        public List<IElement> Context { get; set; } = null;
        public abstract int Priority { get; }

        public abstract double Invoke(double a, double b = 0);

        public string Tostring()
        {
            return Name;
        }
    }
}
