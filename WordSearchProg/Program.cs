using System;
using System.Linq;
using WordSearchLib;

namespace WordSearchProg
{
    class Program
    {   
        static void Main(string[] args)
        {
            const string formatMessage = "Incorrect parameters.\n\nWordSearchProg.exe [targetWord] [columns] [rows]\n\ne.g. WordSearchProg.exe dog 6 6";

            if (args.Count() != 3)
            {
                Console.WriteLine(formatMessage);
            }
            else
            {
                string targetWord = args[0];
                int columns = 0;
                int rows = 0;
                if ((int.TryParse(args[1], out columns) == false) || (int.TryParse(args[2], out rows) == false))
                {
                    Console.WriteLine(formatMessage);
                }
                else
                {
                    try
                    {
                        var MyGenerator = new WordSearchLib.WordSearchGenerator(targetWord, columns, rows);
                        WordSearchGenerator.PrintGrid(MyGenerator.GenerateMatrix().Matrix);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(formatMessage);
                        Console.WriteLine(ex.Message);
                    }
                }                
            }
        }
    }

}
