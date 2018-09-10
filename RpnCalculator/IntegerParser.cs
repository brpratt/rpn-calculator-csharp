using System;

namespace RpnCalculator
{
    public class IntegerParser : IIntegerParser
    {
        public int Parse(string input)
        {
            return int.Parse(input);
        }
    }

    public interface IIntegerParser
    {
        int Parse(string input);
    }
}
