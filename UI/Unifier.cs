using Calculator.Data;
using Calculator.Computator;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculator.UI
{
    class Unifier
    {
        private static IComputator _computator = BasicComputator.getComputator();
        private static IData _memory = new Memory();
        private static IData _history = new History();
        public static Dictionary<string, string> history_dump {
            private set;
            get;
        } = _history.getDataDump();
        public static Dictionary<string, string> memory_dump
        {
            private set;
            get;
        } = _memory.getDataDump();
        static string cleanExpression(string expression)
        {
            return Regex.Replace(expression, @"\s", "");
        }
        static string[] convertToComputatorFormat(string expression)
        {
            var infix = Regex.Split(expression, @"([=!*()\^\/]|(?<!E)[\+\-])").ToList();
            for (int i = infix.Count - 1; i >= 0; i--)
            {
                if (infix[i] == "")
                {
                    infix.RemoveAt(i);
                }
            }
            return infix.ToArray();
        }
        public static string[] getFromMemory(string[] expression)
        {
            var res = new List<string>();
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i][0] == '$' && expression[i][expression[i].Length - 1] == '$')
                {
                    try
                    {
                        res.Add(_memory[expression[i].Trim('$')]);
                    }
                    catch
                    {
                        res.Add("0");
                    }
                }
                else
                {
                    res.Add(expression[i]);
                }
            }
            return res.ToArray();
        }
        public static string calculate(string expression)
        {
            string[] input = null;
            try
            {
                input = convertToComputatorFormat(cleanExpression(expression));
            }
            catch {
                _history[expression] = "unrecognizedToken";
            }
            if (input != null)
            {
                input = getFromMemory(input);
                try
                {
                    _history[expression] = _computator.calculateExpression(input);
                }
                catch (System.DivideByZeroException)
                {
                    _history[expression] = "divideByZero";
                }
                catch (System.StackOverflowException)
                {
                    _history[expression] = "wrongParameters";
                }
                catch(System.Exception e)
                {
                    _history[expression] = e.Message;
                }
                history_dump = _history.getDataDump();
            }
            if (double.TryParse(_history[expression], out _))
            {
                _memory["answer"] = _history[expression];
            }
            return _history[expression];
        }
        public static string calculate(string expression, string name)
        {
            string[] input = null;
            try
            {
                input = convertToComputatorFormat(cleanExpression(expression));
            }
            catch
            {
                _history[expression] = "unrecognizedToken";
            }
            if (input != null)
            {
                input = getFromMemory(input);
                try
                {
                    _history[expression] = _computator.calculateExpression(input);
                }
                catch (System.DivideByZeroException)
                {
                    _history[expression] = "divideByZero";
                }
                catch (System.StackOverflowException)
                {
                    _history[expression] = "wrongParameters";
                }
                catch (System.Exception e)
                {
                    _history[expression] = e.Message;
                }
                history_dump = _history.getDataDump();
            }
            if (double.TryParse(_history[expression], out _))
            {
                _memory["answer"] = _history[expression];
                _memory[name] = _history[expression];
            }
            return _history[expression];
        }
        public static void loadAll()
        {
            _history.loadFromDisk();
            _memory.loadFromDisk();
            history_dump = _history.getDataDump();
            memory_dump = _memory.getDataDump();
        }
        public static void saveAll()
        {
            _history.save();
            _memory.save();
        }
    }
}