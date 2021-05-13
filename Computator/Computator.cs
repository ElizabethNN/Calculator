using System.Collections.Generic;
using System.Linq;

namespace Calculator.Computator
{
    
    class BasicComputator : IComputator
    {
        static BasicComputator _self;
        static public readonly string[] prefix_functions = { "neg", "sin", "cos", "tg", "ctg", "sqrt", "log", "lg", "ln", "abs", "asin", "acos", "atg", "actg" };
        static public readonly string[] postfix_functions = { "!" };
        static public readonly string[] high_priority_functions = { "^" };
        static public readonly string[] medium_priority_functions = { "*", "/" };
        static public readonly string[] low_priority_functions = { "+", "-" };

        private BasicComputator() { }

        string computate(string[] postfix_expression)
        {
            var numbers = new Stack<string>();
            foreach (string i in postfix_expression)
            {
                if (double.TryParse(i, out _))
                {
                    numbers.Push(i);
                }
                else if (Functions.unary_functions.ContainsKey(i))
                {
                    double x;
                    try
                    {
                        double.TryParse(numbers.Pop(), out x);
                    }
                    catch (System.Exception e)
                    {
                        throw new System.Exception("Not enough parameters", e);
                    }
                    numbers.Push(Functions.unary_functions[i](x).ToString());
                }
                else if (Functions.binary_functions.ContainsKey(i))
                {
                    double x, y;
                    try
                    {
                        double.TryParse(numbers.Pop(), out y);
                        double.TryParse(numbers.Pop(), out x);
                    }
                    catch (System.Exception e)
                    {
                        throw new System.Exception("Not enough parameters", e);
                    }
                    numbers.Push(Functions.binary_functions[i](x, y).ToString());
                }
            }
            return numbers.Pop();
        }

        public static BasicComputator getComputator()
        {
            if (_self == null)
            {
                _self = new BasicComputator();
            }
            return _self;
        }

        string[] convertToPostfixExpression(string[] inf_expression)
        {
            var infix_expression = inf_expression.ToList();
            var stack = new Stack<string>();
            var result = new List<string>();
            for (int i = infix_expression.Count - 1; i >=0; i--)
            {
                if (infix_expression[i] == "-" && (i == 0 || !double.TryParse(infix_expression[i - 1], out _)))
                {
                    infix_expression[i] = "neg";
                }
                if (infix_expression[i] == "+" && (i == 0 || !double.TryParse(infix_expression[i - 1], out _)))
                {
                    infix_expression.RemoveAt(i);
                }
            }
            foreach (string i in infix_expression)
            {
                if (double.TryParse(i, out _))
                {
                    result.Add(i);
                }
                else if (postfix_functions.Contains(i))
                {
                    result.Add(i.ToLower());
                }
                else if (prefix_functions.Contains(i) || i == "(")
                {
                    stack.Push(i.ToLower());
                }
                else if (i == ")")
                {
                    string x;
                    while (stack.Count > 0 && (x = stack.Pop()) != "(")
                    {
                        result.Add(x);
                    }
                }
                else if (high_priority_functions.Contains(i))
                {
                    while (stack.Count > 0 && (prefix_functions.Contains(stack.Peek()) || high_priority_functions.Contains(stack.Peek())))
                    {
                        result.Add(stack.Peek());
                        stack.Pop();
                    }
                    stack.Push(i.ToLower());
                }
                else if (medium_priority_functions.Contains(i))
                {
                    while (stack.Count > 0 && (prefix_functions.Contains(stack.Peek()) || high_priority_functions.Contains(stack.Peek()) || medium_priority_functions.Contains(stack.Peek())))
                    {
                        result.Add(stack.Peek());
                        stack.Pop();
                    }
                    stack.Push(i.ToLower());
                }
                else if (low_priority_functions.Contains(i))
                {
                    while (stack.Count > 0 && (prefix_functions.Contains(stack.Peek()) || high_priority_functions.Contains(stack.Peek()) || medium_priority_functions.Contains(stack.Peek()) || low_priority_functions.Contains(stack.Peek())))
                    {
                        result.Add(stack.Peek());
                        stack.Pop();
                    }
                    stack.Push(i.ToLower());
                }
                else
                {
                    throw new System.Exception("Unrecognized symbol");
                }
            }
            while (stack.Count > 0)
            {
                string x = stack.Pop();
                if (x != "(")
                {
                    result.Add(x);
                }
            }
            return result.ToArray();
        }

        public string calculateExpression(string[] infix_expression)
        {
            string[] postfix_expression = convertToPostfixExpression(infix_expression);
            return computate(postfix_expression);
        }
    }
}
