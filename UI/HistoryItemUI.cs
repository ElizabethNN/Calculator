using System.Windows;
using System.Windows.Controls;

namespace Calculator.UI
{
    class HistoryItemUI : Button
    {
        public string expression
        {
            get; private set;
        }
        public string result
        {
            get; private set;
        }
        public HistoryItemUI(string expression, string result, RoutedEventHandler handler, Style style)
        {
            Click += handler;
            this.expression = expression;
            this.result = result;
            Style = style;
            Content = new WrapPanel() { Style = style};
            ((WrapPanel)Content).Children.Add(new TextBlock { Text = expression, Style = style });
            ((WrapPanel)Content).Children.Add(new TextBlock { Text = result, Style = style });
        }
    }
}