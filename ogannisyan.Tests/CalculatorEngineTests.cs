using Microsoft.VisualStudio.TestTools.UnitTesting;
using ogannisyan.Core;
using System;
using System.Globalization;
using System.Threading;

namespace ogannisyan.Tests
{
    [TestClass]
    public class CalculatorEngineTests
    {
        private CalculatorEngine _calculator;

        [TestInitialize]
        public void SetUp()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
            _calculator = new CalculatorEngine();
        }

        #region Тесты сложения

        [TestMethod]
        public void Calculate_Addition_TwoPositiveNumbers_ReturnsCorrectSum()
        {
            double a = 5.0;
            double b = 3.0;
            double expected = 8.0;

            double result = _calculator.Calculate(a, b, "+");

            Assert.AreEqual(expected, result, 0.001);
        }

        [TestMethod]
        public void Calculate_Addition_NegativeNumbers_ReturnsCorrectSum()
        {
            double result = _calculator.Calculate(-5.0, -3.0, "+");
            Assert.AreEqual(-8.0, result, 0.001);
        }

        #endregion

        #region Тесты вычитания

        [TestMethod]
        public void Calculate_Subtraction_PositiveNumbers_ReturnsCorrectDifference()
        {
            double result = _calculator.Calculate(10.0, 4.0, "-");
            Assert.AreEqual(6.0, result, 0.001);
        }

        #endregion

        #region Тесты умножения

        [TestMethod]
        public void Calculate_Multiplication_TwoNumbers_ReturnsCorrectProduct()
        {
            double result = _calculator.Calculate(7.0, 6.0, "*");
            Assert.AreEqual(42.0, result, 0.001);
        }

        #endregion

        #region Тесты деления

        [TestMethod]
        public void Calculate_Division_TwoNumbers_ReturnsCorrectQuotient()
        {
            double result = _calculator.Calculate(100.0, 4.0, "/");
            Assert.AreEqual(25.0, result, 0.001);
        }

        [TestMethod]
        public void Calculate_Division_ByZero_ThrowsDivideByZeroException()
        {
            Assert.ThrowsException<DivideByZeroException>(() =>
                _calculator.Calculate(10.0, 0.0, "/"));
        }

        #endregion

        #region Тесты возведения в степень

        [TestMethod]
        public void Calculate_Power_PositiveExponent_ReturnsCorrectResult()
        {
            double result = _calculator.Calculate(2.0, 3.0, "^");
            Assert.AreEqual(8.0, result, 0.001);
        }

        [TestMethod]
        public void Calculate_Power_ZeroExponent_ReturnsOne()
        {
            double result = _calculator.Calculate(5.0, 0.0, "^");
            Assert.AreEqual(1.0, result, 0.001);
        }

        [TestMethod]
        public void Calculate_Power_NegativeExponent_ReturnsCorrectResult()
        {
            double result = _calculator.Calculate(2.0, -1.0, "^");
            Assert.AreEqual(0.5, result, 0.001);
        }

        #endregion

        #region Тесты обработки ошибок

        [TestMethod]
        public void Calculate_UnknownOperation_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                _calculator.Calculate(5.0, 3.0, "%"));
        }

        #endregion

        #region Тесты парсинга чисел

        [TestMethod]
        public void TryParseNumber_ValidNumber_ReturnsTrue()
        {
            bool result = _calculator.TryParseNumber("123.45", out double value);
            Assert.IsTrue(result);
            Assert.AreEqual(123.45, value, 0.001);
        }

        [TestMethod]
        public void TryParseNumber_InvalidNumber_ReturnsFalse()
        {
            bool result = _calculator.TryParseNumber("abc", out double value);
            Assert.IsFalse(result);
        }

        #endregion
    }
}