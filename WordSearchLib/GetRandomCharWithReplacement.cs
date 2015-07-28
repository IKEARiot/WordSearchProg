using System;
using System.Linq;

namespace WordSearchLib
{
    public class GetRandomCharWithReplacement
    {
        static Random ImSoRandom = new Random();
        char[] _chars;

        public GetRandomCharWithReplacement(string word)
        {
            _chars = word.Distinct().ToArray();
        }

        public GetRandomCharWithReplacement(char[] chars)
        {
            _chars = chars.Distinct().ToArray();
        }

        public int UniqueCharCount()
        {
            return _chars.Count();
        }

        public char GetChar()
        {
            char result;
            var charIndex = ImSoRandom.Next(0, _chars.Count());
            result = _chars[charIndex];
            return result;
        }
    }
}
