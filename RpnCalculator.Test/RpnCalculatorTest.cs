using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace RpnCalculator.Test
{
    [TestClass]
    public class RpnCalculatorTest
    {
        private const int MIN_VALUE = -100000;
        private const int MAX_VALUE = 10000;

        private Mock<IIntegerParser> _parser;
        private RpnCalculator _rpn;
        private Random _random;

        [TestInitialize]
        public void BeforeEach()
        {
            _parser = new Mock<IIntegerParser>();
            _rpn = new RpnCalculator(_parser.Object);
            _random = new Random(DateTime.Now.Millisecond);

            _parser
                .Setup(parser => parser.Parse(It.IsAny<string>()))
                .Returns((string input) => int.Parse(input));
        }

        [TestMethod]
        public void RpnCalculator_WhenGivenSingleNumber_ShouldAddToStack()
        {
            var num = _random.Next();
            _parser.Setup(parser => parser.Parse(num.ToString())).Returns(num);

            _rpn.Enter(num.ToString());

            Assert.AreEqual(1, _rpn.Stack.Count);
            Assert.AreEqual(num, _rpn.Stack.Peek());
        }

        [TestMethod]
        public void RpnCalculator_WhenGivenMultipleNumbers_ShouldAddAllToStack()
        {
            var nums = Enumerable.Range(0, _random.Next(2, 10)).Select(x => _random.Next());

            int count = 0;
            foreach (var num in nums)
            {
                _parser.Setup(parser => parser.Parse(num.ToString())).Returns(num);
                _rpn.Enter(num.ToString());
                count++;

                Assert.AreEqual(count, _rpn.Stack.Count);
                Assert.AreEqual(num, _rpn.Stack.Peek());
            }
        }

        [TestMethod]
        public void RpnCalculator_WhenGivenSingleNumber_UsesIntegerParser()
        {
            var input = _random.Next().ToString();
            var parsedNum = _random.Next();
            _parser.Setup(parser => parser.Parse(input)).Returns(parsedNum);

            _rpn.Enter(input);
            
            Assert.AreEqual(parsedNum, _rpn.Stack.Peek());
            _parser.Verify(parser => parser.Parse(input), Times.Once);
        }

        [TestMethod]
        public void RpnCalculator_WhenGivenMultipleNumbers_UsesIntegerParserForAll()
        {
            var nums = Enumerable.Range(0, _random.Next(5, 10)).Select(num => num.ToString());

            foreach (var num in nums)
            {
                var parsedNum = _random.Next();
                _parser.Setup(parser => parser.Parse(num)).Returns(parsedNum);

                _rpn.Enter(num);

                Assert.AreEqual(parsedNum, _rpn.Stack.Peek());
                _parser.Verify(parser => parser.Parse(num), Times.Once);
            }
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void RpnCalculator_WhenGivenUnknownInput_ThrowsException()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var input = new string(Enumerable.Range(2, _random.Next(10)).Select(x => chars[x]).ToArray());
            
            _rpn.Enter(input);
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void RpnCalculator_AdditionAndStackEmpty_ThrowsException()
        {
            _rpn.Enter("+");
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void RpnCalculator_AdditionAndOneElementInStack_ThrowsException()
        {
            _rpn.Enter(_random.Next().ToString());
            _rpn.Enter("+");
        }

        [TestMethod]
        public void RpnCalculator_AdditionAndSufficientStack_PerformsAddition()
        {
            var num1 = _random.Next(MIN_VALUE, MAX_VALUE);
            var num2 = _random.Next(MIN_VALUE, MAX_VALUE);

            _rpn.Enter(num1.ToString());
            _rpn.Enter(num2.ToString());
            _rpn.Enter("+");

            Assert.AreEqual(1, _rpn.Stack.Count);
            Assert.AreEqual(num1 + num2, _rpn.Stack.Peek());
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void RpnCalculator_MultiplicationAndStackEmpty_ThrowsException()
        {
            _rpn.Enter("*");
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void RpnCalculator_MultiplicationAndOneElementInStack_ThrowsException()
        {
            _rpn.Enter(_random.Next().ToString());
            _rpn.Enter("*");
        }

        [TestMethod]
        public void RpnCalculator_MultiplicationAndSufficientStack_PerformsAddition()
        {
            var num1 = _random.Next(MIN_VALUE, MAX_VALUE);
            var num2 = _random.Next(MIN_VALUE, MAX_VALUE);

            _rpn.Enter(num1.ToString());
            _rpn.Enter(num2.ToString());
            _rpn.Enter("*");

            Assert.AreEqual(1, _rpn.Stack.Count);
            Assert.AreEqual(num1 * num2, _rpn.Stack.Peek());
        }
    }
}
