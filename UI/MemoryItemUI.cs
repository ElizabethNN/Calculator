using System.Windows;
using System.Windows.Controls;

namespace Calculator.UI
{
    class MemoryItemUI : Button
    {
        string name;
        string value;
        public MemoryItemUI(string name, string value, RoutedEventHandler handler)
        {
            Click += handler;
            this.name = name;
            this.value = value;
            Content = new WrapPanel();
            ((WrapPanel)Content).Children.Add(new TextBlock { Text = name });
            ((WrapPanel)Content).Children.Add(new TextBlock { Text = value });
        }
    }
}