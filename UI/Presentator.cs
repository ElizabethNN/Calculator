using Calculator.Memory;
using Calculator.Computator;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Calculator.UI
{
    class Presentator
    {
        static readonly IComputator _computator = Computator.Computator.getComputator();
        static readonly IMemory _memory = new Memory.Memory("data");
        static readonly IMemory _history = new History("history");
        public static Dictionary<string, string> memorydump { private set; get; } = _memory.getMemoryDump();
        public static Dictionary<string, string> historydump { private set; get; } = _memory.getMemoryDump();

        public static string cleanExpression(string expression)
        {
            return Regex.Replace(expression, @"\s", "");
        }

        public static Dictionary<string, string> getMemoryDump() {
            return _memory.getMemoryDump();
        }

        public static string[] convertToComputatorFormat(string expression)
        {
            var infix = Regex.Split(expression, @"([=!*()\^\/]|(?<!E)[\+\-])").ToList();
            for (int i = infix.Count - 1; i >=0; i--)
            {
                if (infix[i] == "")
                {
                    infix.RemoveAt(i);
                }
            }
            return infix.ToArray();
        }

        public static string getFromHistory(string name)
        {
            try
            {
                return _history[name];
            }
            catch
            {
                return "0";
            }
        }

        public static string getFromMemory(string name)
        {
            try
            {
                return _memory[name.Trim('$')];
            }
            catch {
                return "0";
            }
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
                    catch {
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
            string[] input = convertToComputatorFormat(cleanExpression(expression));
            input = getFromMemory(input);
            _history[expression] = _computator.calculateExpression(input);
             historydump = _history.getMemoryDump();
            return _history[expression];
        }

        public static void loadFromDisk()
        {
            _history.loadFromDisk();
            _memory.loadFromDisk();
            historydump = _history.getMemoryDump();
            memorydump = _memory.getMemoryDump();
        }

        public static string calculateWithAssignment(string expression, string name)
        {
            string[] input = convertToComputatorFormat(cleanExpression(expression));
            input = getFromMemory(input);
            _history[expression] = "";
            _memory[name.Trim('$')] = _computator.calculateExpression(input);
            memorydump = _memory.getMemoryDump();
            _history[expression] = _memory[name.Trim('$')];
            historydump = _history.getMemoryDump();
            if (name != "answer")
                _memory["answer"] = _memory[name.Trim('$')];
            return _memory[name.Trim('$')];
        }

        public static void save()
        {
            _history.save();
            _memory.save();
        }
    }
}
