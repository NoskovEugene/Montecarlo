using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Analyzer.Classes;
using Analyzer.Factories;

namespace Analyzer
{
    public class _Analyzer
    {
        public void Show(List<IElement> elements, int index)
        {
            string Pre = string.Empty;
            for (int i = 0; i < index; i++)
            {
                Pre += "-";
            }
            Pre += ">";
            foreach (IElement element in elements)
            {
                if (element.Type == ElementType.Function)
                {
                    Console.WriteLine($"{Pre} {element.Name} : {element.Type}");
                    Show(element.Context, index = index + 1);
                }
                else
                {
                    Console.WriteLine($"{Pre} {element.Name} : {element.Type}");
                }
            }
        }

        public List<IElement> Analyze(string Input)
        {
            List<IElement> elements = new List<IElement>();
            FactoryOperators operators = new FactoryOperators();
            FactoryFunctions functions = new FactoryFunctions();
            int Count = 0;
            while (Input != "" & Count != 1000)
            {
                char first = char.Parse(Input.Substring(0, 1)); //first element from string
                if (char.IsDigit(first)) // number
                {
                    double a = GetVal(Input, @"\-{0,1}\d{1,}[\.\,]{0,1}\d{0,}");
                    elements.Add(new ValueElement(a));
                    Input = Input.Remove(0, a.ToString().Length);
                }
                else if (first == 'x' | first == 'X') // variable
                {
                    elements.Add(new VariableElement());
                    Input = Input.Remove(0, 1);
                }
                else if (first == '(') // open bracket
                {
                    if (Input.Length == 1) { throw new Exception("Error in analyze. Lenght line cannot be 1"); }
                    KeyValuePair<string, double> tmp = TryToGetNegative(Input);
                    if (!double.IsNaN(tmp.Value))
                    {
                        elements.Add(new ValueElement(tmp.Value));
                        Input = Input.Remove(0, tmp.Key.Length);
                    }
                    else
                    {
                        elements.Add(new OpenBracket());
                        Input = Input.Remove(0, 1);
                    }
                }
                else if (first == ')') // close bracket
                {
                    elements.Add(new CloseBracket());
                    Input = Input.Remove(0, 1);
                }
                else if (char.IsLetter(first))
                {
                    string NameFunction = "";
                    string tmp = "";
                    int brackets = 0;
                    do // get function name
                    {
                        NameFunction += tmp;
                        if (Input.Length == 0) throw new Exception("Error in Analyze. Incoming string ended");
                        tmp = Input.Substring(0, 1); //get first element
                        Input = Input.Remove(0, 1); // remove first element from input
                    } while (tmp != "(");
                    brackets++;
                    string Context = "";
                    do // get context function
                    {
                        Context += tmp;
                        if (Input.Length == 0) throw new Exception("Error in Analyze. Incoming string ended");
                        tmp = Input.Substring(0, 1);
                        Input = Input.Remove(0, 1);
                        if (tmp == "(") { brackets++; }
                        if (tmp == ")") { brackets--; }
                    } while (tmp != ")" | brackets > 0);
                    Context = AlignBrackets(Context);
                    if (functions.Contains(NameFunction))
                    {
                        ClassFunction element = functions.Function;
                        element.Context = Analyze(Context);
                        elements.Add(element);
                    }
                    else
                    {
                        throw new Exception($"Error in Analyze. Unexpected value '{NameFunction}'");
                    }

                }
                else if (operators.Contains(first))
                {
                    elements.Add(operators.Operator);
                    Input = Input.Remove(0, operators.Operator.Name.Length);
                }
                else
                {

                    throw new Exception($"Error in Analyze. Unexpected item '{first.ToString()}'");
                }
                Count++;
            }

            return elements;
        }

        public KeyValuePair<string, double> TryToGetNegative(string Input)
        {
            string Tmp = string.Empty;
            if (Input.Length < 2) { return new KeyValuePair<string, double>(string.Empty, double.NaN); }
            string Item = string.Empty;
            do
            {
                Tmp = Input.Substring(0, 1);
                Input = Input.Remove(0, 1);
                Item += Tmp;
                if (Input.Length > 1)
                {
                    string nxt = Input.Substring(0, 1);
                    if (Tmp == "(" & nxt != "-") { return new KeyValuePair<string, double>(string.Empty, double.NaN); }
                }
            } while (Tmp != ")" & Input.Length > 0);
            return new KeyValuePair<string, double>(Item, GetVal(Item.Replace("(", string.Empty).Replace(")", string.Empty), @"\-{1}\d{1,}[\.\,]{0,1}\d{0,}"));
        }

        public string AlignBrackets(string Input)
        {
            int open = 0;
            int close = 0;
            foreach (var item in Input)
            {
                if (item == '(')
                    open++;
                if (item == ')')
                    close++;
            }
            if (open > close)
            {
                for (int i = 0; i < open - close; i++)
                {
                    Input += ")";
                }
                return Input;
            }
            else
            {
                for (int i = 0; i < close - open; i++)
                {
                    Input = "(" + Input;
                }
            }
            return Input;
        }

        public double GetVal(string input, string Pattern)
        {
            Regex regex = new Regex(Pattern);
            Match match = regex.Match(input);
            if (match.Value != "")
            {
                return double.Parse(match.Value.Replace('.', ','));
            }
            else
            {
                return double.NaN;
            }
        }

    }
}
