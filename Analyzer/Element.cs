using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer
{
    /// <summary>
    /// Types elements
    /// </summary>
    [Serializable]
    public enum ElementType
    {
        Null = 0,
        Bracket = 1,
        Value = 2,
        Variable = 3,
        Operator = 4,
        Function = 5,
    }

    /// <summary>
    /// Global interface
    /// </summary>
    public interface IElement
    {
        string Name { get; }

        double Value { get; }

        double Invoke(double a, double b = 0);

        List<IElement> Context { get; set; }

        ElementType Type { get; }

        string Tostring();

        int Priority { get; }
    }
}
