using System;
using System.Collections.Generic;

namespace Analyzer.Classes
{
    /// <summary>
    /// Function element
    /// </summary>
    [Serializable]
    abstract class ClassFunction : IElement
    {
        public abstract string Name { get; }
        public abstract double Value { get; set; }
        public ElementType Type { get; } = ElementType.Function;
        public abstract List<IElement> Context { get; set; }
        public abstract int Priority { get; }

        public abstract double Invoke(double a, double b = 0);

        public double Analyse(double X = double.NaN)
        {
            Stack<IElement> elements = new Stack<IElement>();
            foreach (IElement item in Context)
            {
                if (item.Type == ElementType.Value)
                {
                    elements.Push(item);
                }
                if (item.Type == ElementType.Variable)
                {
                    if (X == double.NaN) { throw new Exception($"Error in {Name} function. Unexpected context "); }
                    elements.Push(new ValueElement(X));
                }
                if (item.Type == ElementType.Operator)
                {
                    if (elements.Count < 2) { throw new Exception("Error in getting elements from stack. Elements count equals zero or one"); }
                    double Bpop = elements.Pop().Value;
                    double Apop = elements.Pop().Value;
                    elements.Push(new ValueElement(item.Invoke(Apop, Bpop)));
                }
                if (item.Type == ElementType.Function)
                {
                    elements.Push(new ValueElement(item.Invoke(X)));
                }
            }
            if (elements.Count > 1) { throw new Exception("Error in analysing or calculating function. Elements count more than one"); }
            return elements.Pop().Value;
        }

        public string Tostring()
        {
            string LineReturn = $"{Name}";
            foreach(var item in Context)
            {
                LineReturn += $"{item.Tostring()}";
            }
            return $"{LineReturn}";
        }
    }
}
