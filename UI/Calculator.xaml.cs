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

namespace Calculator
{
    class HistoryListElement : WrapPanel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string expression;
        public string value
        {
            get {
                return ((TextBlock)Children[1]).Text;
            }
            set {
                ((TextBlock)Children[1]).Text = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Children"));
            }
        }

        public HistoryListElement(string expression, string value)
        {
            Children.Add(new TextBlock
            {
                Text = expression
            });
            Children.Add(new TextBlock
            {
                Text = value
            });
            this.expression = expression;
            Orientation = Orientation.Vertical;
        }
    }

    class MemoryListElement : WrapPanel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string name;
        public string value
        {
            get {
                return ((TextBlock)Children[1]).Text;
            }
            set {
                ((TextBlock)Children[1]).Text = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Children"));
            }
        }

        public MemoryListElement(string name, string value)
        {
            Children.Add(new TextBlock
            {
                Text = name
            });
            Children.Add(new TextBlock {
                Text = value
            });
            this.name = name;
            Orientation = Orientation.Vertical;
        }
    }

    public partial class MainWindow : Window
    {
        delegate void eventsFunc(object sender, RoutedEventArgs e);
        readonly char[] _allowedsymbols = {
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
        ObservableCollection<MemoryListElement> memoryList = new ObservableCollection<MemoryListElement>();
        ObservableCollection<HistoryListElement> historyList = new ObservableCollection<HistoryListElement>();

        public MainWindow()
        {
            InitializeComponent();
            textBox.Focus();
            MemoryList.ItemsSource = memoryList;
            HistoryList.ItemsSource = historyList;
            Presentator.loadFromDisk();
            foreach (var i in Presentator.memorydump.Keys)
            {
                memoryList.Add(new MemoryListElement(i, Presentator.getFromMemory(i)));
            }
            foreach (var i in Presentator.historydump.Keys)
            {
                historyList.Add(new HistoryListElement(i, Presentator.getFromHistory(i)));
            }
            Closed += OnWindowClose;
        }

        string cleanInput(string input)
        {
            string str = "";
            string lowercase = textBox.Text.ToLower();
            for (int i = 0; i < lowercase.Length; i++)
            {
                if (_allowedsymbols.Contains(lowercase[i]))
                {
                    str += textBox.Text[i];
                }
            }
            return str;
        }

        private void onTextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)sender;
            textbox.Text = cleanInput(textbox.Text);
            textbox.CaretIndex = textbox.Text.Length;
        }

        private string[] parseAssignment(string text)
        {
            return text.Split('=');
        }

        private void onEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                calculate();
            }
            else if (e.Key == Key.LeftCtrl)
            {
                Presentator.save();
            }
        }

        private void symbolClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            textBox.Text += button.Content;
        }

        private void calculate()
        {
            string[] assignment = parseAssignment(textBox.Text);
            string result = "";
            if (assignment.Length == 1)
            {
                try
                {
                    result = Presentator.calculateWithAssignment(assignment[0], "answer");
                }
                catch (System.OverflowException)
                {
                    result = "wrongParameters";
                }
                catch (System.DivideByZeroException)
                {
                    result = "divideByZero";
                }
                catch (System.Exception)
                {
                    result = "unrecognizedToken";
                }
            }
            else if (assignment.Length > 1)
            {
                try
                {
                    result = Presentator.calculateWithAssignment(assignment[1], assignment[0]);
                }
                catch
                {
                    result = "error";
                }
            }
            memoryList.Clear();
            foreach (var i in Presentator.memorydump.Keys)
            {
                memoryList.Add(new MemoryListElement(i, Presentator.getFromMemory(i)));
            }
            historyList.Add(new HistoryListElement(textBox.Text, result));
            textBox.Text = result;
            textBox.CaretIndex = textBox.Text.Length;
        }

        private void onEqual(object sender, RoutedEventArgs e)
        {
            calculate();
        }

        private void deleteLast(object sender, RoutedEventArgs e)
        {
            textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
        }

        private void OnWindowClose(object sender, EventArgs e)
        {
            Presentator.save();
        }
    }
}