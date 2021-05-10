using System.Windows;
using System.Windows.Controls;

namespace Calculator.UI
{
    class HistoryItemUI : Button
    {
        string expression;
        string result;
        public HistoryItemUI(string expression, string result, RoutedEventHandler handler)
        {
            Click += handler;
            this.expression = expression;
            this.result = result;
            Content = new WrapPanel();
            ((WrapPanel)Content).Children.Add(new TextBlock { Text = expression });
            ((WrapPanel)Content).Children.Add(new TextBlock { Text = result });
        }
    }
}