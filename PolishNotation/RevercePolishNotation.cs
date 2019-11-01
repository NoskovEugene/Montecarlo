using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analyzer;
using Analyzer.Classes;

namespace PolishNotation
{
    [Serializable]
    public class RevercePolishNotation
    {
        public List<IElement> GetNotation(List<IElement> elements)
        {
            List<IElement> RList = new List<IElement>();
            Stack<IElement> stackElements = new Stack<IElement>();
            int brackets = 0;
            foreach(IElement element in elements)
            {
                if(element.Type == ElementType.Value)
                {
                    RList.Add(element);
                }
                else if (element.Type == ElementType.Bracket)
                {
                    if(element.Name == "(")
                    {
                        brackets++;
                        stackElements.Push(element);
                    }
                    else
                    {
                        if(brackets < 1)
                        {
                            throw new Exception("Bracket not found.Please check brackets and try again");
                        }
                        else
                        {
                            while (stackElements.Peek().Name != "(")
                            {
                                RList.Add(stackElements.Pop());
                            }
                            stackElements.Pop();
                        }
                    }
                }
                else if (element.Type == ElementType.Variable)
                {
                    RList.Add(element);
                }
                else if (element.Type == ElementType.Function)
                {
                    ClassFunction function = (ClassFunction)element;
                    foreach(IElement Item in function.Context)
                    {
                        if(Item.Type == ElementType.Function)
                        {
                            Item.Context = GetNotation(Item.Context);
                        }
                    }
                    function.Context = GetNotation(function.Context);
                    RList.Add(function);
                }
                else if(element.Type == ElementType.Operator)
                {
                    if(stackElements.Count != 0)
                    {
                        IElement last = stackElements.Peek();
                        if(last.Priority < element.Priority)
                        {
                            stackElements.Push(element);
                        }
                        else
                        {
                            RList.Add(stackElements.Pop());
                            stackElements.Push(element);
                        }
                    }
                    else
                    {
                        stackElements.Push(element);
                    }
                }
            }
            while(stackElements.Count > 0)
            {
                RList.Add(stackElements.Pop());
            }
            return RList;
        }
    }
}
