using System;
using System.Collections.Generic;

namespace RpnCalculator
{
    public class RpnCalculator
    {
        private const string UnknownOperationExceptionMessage = "Unknown operation.";
        private const string StackLengthExceptionMessage = "Too few elements on stack.";

        public Stack<int> Stack { get; }

        private readonly IIntegerParser _parser;

        public RpnCalculator(IIntegerParser parser)
        {
            Stack = new Stack<int>();
            _parser = parser;
        }

        public void Enter(string input)
        {
            switch (input)
            {
                case "+":
                    Add();
                    break;
                case "*":
                    Multiply();
                    break;
                default:
                    int parsed;
                    try
                    {
                        parsed = _parser.Parse(input);
                    }
                    catch (Exception)
                    {
                        throw new Exception(UnknownOperationExceptionMessage);
                    }

                    Stack.Push(parsed);
                    break;
            }
        }

        private void Add()
        {
            if (Stack.Count < 2)
            {
                throw new Exception(StackLengthExceptionMessage);
            }

            var num1 = Stack.Pop();
            var num2 = Stack.Pop();
            Stack.Push(num1 + num2);
        }

        private void Multiply()
        {
            if (Stack.Count < 2)
            {
                throw new Exception(StackLengthExceptionMessage);
            }

            var num1 = Stack.Pop();
            var num2 = Stack.Pop();
            Stack.Push(num1 * num2);
        }
    }
}
