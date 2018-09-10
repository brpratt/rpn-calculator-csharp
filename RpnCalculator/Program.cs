using System;
using System.Linq;

namespace RpnCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var rpn = new RpnCalculator();

            while (true)
            {
                Console.Write("> ");

                try
                {
                    rpn.Enter(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                PrintStack(rpn);
            }
        }

        static void PrintStack(RpnCalculator rpn)
        {
            var numbers = rpn.Stack.Reverse().ToArray();
            Console.WriteLine($"stack: [{string.Join(" ", numbers)}]");
        }
    }
}
