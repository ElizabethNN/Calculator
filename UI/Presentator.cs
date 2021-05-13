using System.Windows.Controls;
using System.Windows;
using System.Collections.Generic;
using Calculator.Computator;
using System.Linq;

namespace Calculator.UI
{
    class Presentator
    {
        static readonly char[] _allowedsymbols = {
            'a',
            'b',
            'c',
            'd',
            'e',
            'f',
            'g',
            'h',
            'i',
            'j',
            'k',
            'l',
            'm',
            'n',
            'o',
            'p',
            'q',
            'r',
            's',
            't',
            'u',
            'v',
            'w',
            'x',
            'y',
            'z',
            '1',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9',
            '0',
            '(',
            ')',
            '+',
            '-',
            '*',
            '/',
            '!',
            '$',
            '=',
            '^',
            ','
        };
        public static List<MemoryItemUI> updateMemory(RoutedEventHandler handler, Style style)
        {
            var Elements = new List<MemoryItemUI>();
            foreach (KeyValuePair<string, string> i in Unifier.memory_dump)
            {
                Elements.Add(new MemoryItemUI(i.Key, i.Value, handler, style));
            }
            return Elements;
        }
        public static List<HistoryItemUI> updateHistory(RoutedEventHandler handler, Style style)
        {
            var Elements = new List<HistoryItemUI>();
            foreach (KeyValuePair<string, string> i in Unifier.history_dump)
            {
                Elements.Add(new HistoryItemUI(i.Key, i.Value, handler, style));
            }
            return Elements;
        }
        public static string cleanInput(string input, int old_pos, out int new_pos)
        {
            string str = "";
            new_pos = old_pos;
            string lowercase = input.ToLower();
            for (int i = 0; i < lowercase.Length; i++)
            {
                if (_allowedsymbols.Contains(lowercase[i]))
                {
                    str += input[i];
                }
                else if (i < old_pos)
                {
                    new_pos--;
                }
            }
            return str;
        }
        public static string[] parseAssignment(string text)
        {
            return text.Split('=');
        }
    }
}
