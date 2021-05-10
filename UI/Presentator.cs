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
        public static UIElementCollection updateMemory(FrameworkElement element, RoutedEventHandler handler)
        {
            var iElementCollection = new UIElementCollection(element, element);
            foreach (KeyValuePair<string, string> i in Unifier.memory_dump)
            {
                iElementCollection.Add(new MemoryItemUI(i.Key, i.Value, handler));
            }
            return iElementCollection;
        }
        public static UIElementCollection updateHistory(FrameworkElement element, RoutedEventHandler handler)
        {
            var iElementCollection = new UIElementCollection(element, element);
            foreach (KeyValuePair<string, string> i in Unifier.history_dump)
            {
                iElementCollection.Add(new MemoryItemUI(i.Key, i.Value, handler));
            }
            return iElementCollection;
        }
        public static string cleanInput(string input)
        {
            string str = "";
            string lowercase = input.ToLower();
            for (int i = 0; i < lowercase.Length; i++)
            {
                if (_allowedsymbols.Contains(lowercase[i]))
                {
                    str += input[i];
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
