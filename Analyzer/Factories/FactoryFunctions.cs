using System;
using System.Collections.Generic;
using Analyzer;
using Analyzer.Functions;
using Analyzer.Classes;


namespace Analyzer.Factories
{
    /// <summary>
    /// Factory function. Find function at name
    /// </summary>
    [Serializable]
    class FactoryFunctions
    {
        public ClassFunction Function { get; private set; }

        public List<ClassFunction> Operators = new List<ClassFunction>()
        {
            new Sin(),
            new ArcSin(),
            new Cos(),
            new ArcCos(),
            new Tan(),
            new ArcTan(),
            new Ctg(),
            new ArcCtg(),
            new Sqrt(),
            new Sec(),
            new CoSec(),
            new LogE(),
            new Log10()
        };

        public bool Contains(string Input)
        {
            foreach (ClassFunction item in Operators)
            {
                if (Input.Equals(item.Name, StringComparison.OrdinalIgnoreCase))
                {
                    Type extention = item.GetType();
                    Function = (ClassFunction)Activator.CreateInstance(extention);
                    return true;
                }
            }
            return false;
        }

    }
}
