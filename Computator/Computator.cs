﻿using System.Collections.Generic;
using System.Linq;

namespace Calculator.Computator
{
    
    class Computator : IComputator
    {
        static public readonly string[] prefix_functions = { "neg", "sin", "cos", "tg", "ctg" };
        static public readonly string[] postfix_functions = { "!" };
        static public readonly string[] high_priority_functions = { "^" };
        static public readonly string[] medium_priority_functions = { "*", "/" };
        static public readonly string[] low_priority_functions = { "+", "-" };

        static string computate(string[] postfix_expression)
        {
            var numbers = new Stack<string>();
            foreach (string i in postfix_expression)
            {
                if (decimal.TryParse(i, out _))
                {
                    numbers.Push(i);
                }
                else if (Functions.unary_functions.ContainsKey(i))
                {
                    decimal.TryParse(numbers.Pop(), out decimal x);
                    numbers.Push(Functions.unary_functions[i](x).ToString());
                }
                else if (Functions.binary_functions.ContainsKey(i))
                {
                    decimal.TryParse(numbers.Pop(), out decimal x);
                    decimal.TryParse(numbers.Pop(), out decimal y);
                    numbers.Push(Functions.binary_functions[i](x, y).ToString());
                }
            }
            return numbers.Pop();
        }

        static string[] convertToPostfixExpression(string[] infix_expression)
        {
            var stack = new Stack<string>();
            var result = new List<string>();
            for (int i = 0; i < infix_expression.Length; i++)
            {
                if (infix_expression[i] == "-" && (i == 0 || !decimal.TryParse(infix_expression[i - 1], out _)))
                {
                    infix_expression[i] = "neg";
                }
            }
            foreach (string i in infix_expression)
            {
                if (decimal.TryParse(i, out _))
                {
                    result.Add(i);
                }
                else if (postfix_functions.Contains(i))
                {
                    result.Add(i);
                }
                else if (prefix_functions.Contains(i))
                {
                    stack.Push(i);
                }
                else if (i == ")")
                {
                    string x;
                    while ((x = stack.Pop()) != "(")
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
                    stack.Push(i);
                }
                else if (medium_priority_functions.Contains(i))
                {
                    while (stack.Count > 0 && (prefix_functions.Contains(stack.Peek()) || high_priority_functions.Contains(stack.Peek()) || medium_priority_functions.Contains(stack.Peek())))
                    {
                        result.Add(stack.Peek());
                        stack.Pop();
                    }
                    stack.Push(i);
                }
                else if (low_priority_functions.Contains(i))
                {
                    while (stack.Count > 0 && (prefix_functions.Contains(stack.Peek()) || high_priority_functions.Contains(stack.Peek()) || medium_priority_functions.Contains(stack.Peek()) || low_priority_functions.Contains(stack.Peek())))
                    {
                        result.Add(stack.Peek());
                        stack.Pop();
                    }
                    stack.Push(i);
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
