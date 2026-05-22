using System;
using System.Windows;
using System.Windows.Controls;
using ogannisyan.Core;

namespace ogannisyan.Calculator
{
    public partial class MainWindow : Window
    {
        private readonly CalculatorEngine _calculator;
        private double _firstNumber = 0;
        private string _operation = "";
        private bool _isOperationPerformed = false;

        public MainWindow()
        {
            InitializeComponent();
            _calculator = new CalculatorEngine();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Content.ToString();

            if (buttonText.Length == 1 && (char.IsDigit(buttonText[0]) || buttonText == "."))
            {
                if (_isOperationPerformed || Display.Text == "0")
                {
                    Display.Text = "";
                    _isOperationPerformed = false;
                }

                if (buttonText == "." && Display.Text.Contains("."))
                    return;

                Display.Text += buttonText;
            }
            else if (buttonText == "+" || buttonText == "-" || buttonText == "*" ||
                     buttonText == "/" || buttonText == "^")
            {
                if (Display.Text != "")
                {
                    if (_calculator.TryParseNumber(Display.Text, out double result))
                    {
                        _firstNumber = result;
                        _operation = buttonText;
                        _isOperationPerformed = true;
                    }
                }
            }
            else if (buttonText == "=")
            {
                if (_operation != "" && Display.Text != "")
                {
                    try
                    {
                        if (_calculator.TryParseNumber(Display.Text, out double secondNumber))
                        {
                            double result = _calculator.Calculate(_firstNumber, secondNumber, _operation);
                            Display.Text = result.ToString();
                            _operation = "";
                            _isOperationPerformed = true;
                        }
                    }
                    catch (DivideByZeroException ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        ClearAll();
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }            
            else if (buttonText == "C")
            {
                ClearAll();
            }
        }

        private void ClearAll()
        {
            Display.Text = "0";
            _firstNumber = 0;
            _operation = "";
            _isOperationPerformed = false;
        }
    }
}