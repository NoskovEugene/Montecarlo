using System;
using System.Collections.Generic;

namespace Analyzer.Classes
{
    /// <summary>
    /// Variable element
    /// </summary>
    [Serializable]
    class VariableElement : IElement
    {
        public string Name => "X";

        public double Value => double.NaN;

        public List<IElement> Context { get; set; } = null;

        public ElementType Type => ElementType.Variable;

        public int Priority => -1;

        public double Invoke(double a, double b = 0)
        {
            return double.NaN;
        }

        public string Tostring()
        {
            return Name;
        }
    }
}
