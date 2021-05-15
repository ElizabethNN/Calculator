using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using Calculator.UI;
using System.Linq;
using System.Windows.Input;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System;
using Calculator.Computator;

namespace Calculator
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            textBox.Focus();
            Unifier.loadAll();
            MemoryList.Children.Clear();
            foreach (object i in Presentator.updateMemory(onMemoryClick, FindResource("SidePanel") as Style))
            {
                MemoryList.Children.Add((UIElement)i);
            }
            foreach (object i in Presentator.updateHistory(onHistoryClick, FindResource("SidePanel") as Style))
            {
                HistoryList.Children.Add((UIElement)i);
            }
            Closed += OnWindowClose;
        }
        private void onTextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)sender;
            textbox.Text = Presentator.cleanInput(textbox.Text, textBox.CaretIndex, out int index);
            textbox.CaretIndex = index;
        }
        private void onKeyboardKeys(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                calculate();
            }
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.S))
            {
                Unifier.saveAll();
            }
        }
        private void symbolClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            textBox.Text += button.Content;
        }
        private void calculate()
        {
            string[] assignment = Presentator.parseAssignment(textBox.Text);
            string result = "";
            if (assignment.Length == 1)
            {
                result = Unifier.calculate(assignment[0]);
            }
            else if (assignment.Length > 1)
            {
                result = Unifier.calculate(assignment[1], assignment[0]);
            }
            MemoryList.Children.Clear();
            foreach (object i in Presentator.updateMemory(onMemoryClick, FindResource("SidePanel") as Style))
            {
                MemoryList.Children.Add((UIElement)i);
            }
            HistoryList.Children.Clear();
            foreach (object i in Presentator.updateHistory(onHistoryClick, FindResource("SidePanel") as Style))
            {
                HistoryList.Children.Add((UIElement)i);
            }
            textBox.Text = result;
            textBox.CaretIndex = textBox.Text.Length;
        }
        private void onEqual(object sender, RoutedEventArgs e)
        {
            calculate();
        }
        private void deleteLast(object sender, RoutedEventArgs e)
        {
            textBox.Text = textBox.Text.Length==0 ? "": textBox.Text.Remove(textBox.Text.Length - 1);
        }
        private void OnWindowClose(object sender, EventArgs e)
        {
            Unifier.saveAll();
        }
        private void onMemoryClick(object sender, EventArgs e)
        {
            var element = (MemoryItemUI)sender;
            textBox.Text += "$" + element.name + "$";
            textBox.CaretIndex = textBox.Text.Length;
        }
        private void onHistoryClick(object sender, EventArgs e)
        {
            var element = (HistoryItemUI)sender;
            textBox.Text = element.expression;
            textBox.CaretIndex = textBox.Text.Length;
        }
        private void onTabClick(object sender, RoutedEventArgs e)
        {
            textBox.Focus();
        }
    }
}