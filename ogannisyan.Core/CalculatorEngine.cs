using System;

namespace ogannisyan.Core
{

    public class CalculatorEngine
    {
        public double Calculate(double firstNumber, double secondNumber, string operation)
        {
            switch (operation)
            {
                case "+":
                    return firstNumber + secondNumber;

                case "-":
                    return firstNumber - secondNumber;

                case "*":
                    return firstNumber * secondNumber;

                case "/":
                    if (secondNumber == 0)
                        throw new DivideByZeroException("Деление на ноль невозможно!");
                    return firstNumber / secondNumber;

                case "^":
                    return Math.Pow(firstNumber, secondNumber);

                default:
                    throw new ArgumentException($"Неизвестная операция: {operation}");
            }
        }

        public bool TryParseNumber(string text, out double result)
        {
            return double.TryParse(text, out result);
        }
    }
}