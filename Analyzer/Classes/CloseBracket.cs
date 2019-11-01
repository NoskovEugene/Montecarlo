using System;
using System.Collections.Generic;

namespace Analyzer.Classes
{
    /// <summary>
    /// Close bracket
    /// </summary>
    [Serializable]
    class CloseBracket : IElement
    {
        public string Name { get; } = ")";

        public double Value => double.NaN;

        public List<IElement> Context { get; set; } = null;

        public ElementType Type => ElementType.Bracket;

        public int Priority => 0;

        public double Invoke(double a, double b = 0)
        {
            return a;
        }

        public string Tostring()
        {
            return Name;
        }


    }
}
