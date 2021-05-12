using System.Windows;
using System.Windows.Controls;

namespace Calculator.UI
{
    class MemoryItemUI : Button
    {
        public string name { get; private set; }
        public string value  { get; private set; }
        public MemoryItemUI(string name, string value, RoutedEventHandler handler, Style style)
        {
            Click += handler;
            this.name = name;
            this.value = value;
            Style = style;
            Content = new WrapPanel() { Style = style };
            ((WrapPanel)Content).Children.Add(new TextBlock { Text = name, Style = style });
            ((WrapPanel)Content).Children.Add(new TextBlock { Text = value, Style = style });
        }
    }
}