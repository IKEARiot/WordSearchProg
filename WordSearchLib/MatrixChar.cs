using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearchLib
{

    public struct MatrixChar
    {
        public char Char;
        public bool Protected;
        public string Charhistory;

        public MatrixChar(char thisChar, bool protectedValue)
        {
            Char = thisChar;
            Protected = protectedValue;
            Charhistory = "";
        }
        public MatrixChar(char thisChar)
        {
            Char = thisChar;
            Protected = false;
            Charhistory = "";
        }
    }
}
