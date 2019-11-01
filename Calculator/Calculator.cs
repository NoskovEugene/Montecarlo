using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analyzer;
using Analyzer.Classes;

namespace Calculator
{
    [Serializable]
    public class Calculator
    {
        public double Calc(List<IElement> PolExpression, double X = double.NaN)
        {
            Stack<IElement> Value = new Stack<IElement>();
            foreach (IElement item in PolExpression)
            {
                if (item.Type == ElementType.Value)
                {

                    Value.Push(item);
                }
                else if(item.Type == ElementType.Variable)
                {
                    Value.Push(new ValueElement(X));
                }
                else if(item.Type == ElementType.Function)
                {
                    Value.Push(new ValueElement(item.Invoke(X)));
                }
                else if(item.Type == ElementType.Operator)
                {
                    if (Value.Count < 2) throw new Exception($"Error in calculate. Element for operation '{item.Name}' not found");
                    IElement b = Value.Pop();
                    IElement a = Value.Pop();
                    Value.Push(new ValueElement(item.Invoke(a.Value, b.Value)));
                }
            }

            if (Value.Count > 1) throw new Exception($"Element in stack more than one");
            if (Value.Count == 0) throw new Exception($"Stack is empty");
            return Value.Pop().Value;
        }

    }
}
