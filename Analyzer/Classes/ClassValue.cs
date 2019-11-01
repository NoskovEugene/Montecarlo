using System;
using System.Collections.Generic;

namespace Analyzer.Classes
{
    /// <summary>
    /// Abstract numeric element
    /// </summary>
    [Serializable]
    abstract class ClassValue : IElement
    {
        public abstract string Name { get; protected set; }
        public abstract double Value { get; protected set; }
        public List<IElement> Context { get; set; } = null;
        public ElementType Type { get; } = ElementType.Value;

        public int Priority => -1;

        public double Invoke(double a = 0, double b = 0) => a;

        public string Tostring()
        {
            return Name;
        }
    }
}
