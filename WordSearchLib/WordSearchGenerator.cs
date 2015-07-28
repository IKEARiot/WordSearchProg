using System;
using System.Collections.Generic;
using System.Linq;

namespace WordSearchLib
{
    public class WordSearchGenerator
    {
        private string _TargetWord = "";
        private int _MatrixSizeX = 0;
        private int _MatrixSizeY = 0;

        private MatrixChar[,] _myMatrix;

        public WordSearchGenerator(string targetWord, int matrixSizeX, int matrixSizeY)
        {
            if (targetWord.Length < 3)
            {
                throw new ArgumentException("Target word must be more than 2 characters.");
            }
            if (matrixSizeX < 3)
            {
                throw new ArgumentException("Matrix column size must be greater than 2.");
            }
            if (matrixSizeY < 3)
            {
                throw new ArgumentException("Matrix row size must be greater than 2.");
            }
            if (targetWord.Length > matrixSizeX || targetWord.Length > matrixSizeY)
            {
                throw new ArgumentException("Target word does not fit in matrix.");
            }
            if (targetWord.ToCharArray().Distinct().Count() < 3)
            {
                throw new ArgumentException("The target word must have more than 2 unique characters.");
            }

            this._TargetWord = targetWord;
            this._MatrixSizeX = matrixSizeX;
            this._MatrixSizeY = matrixSizeY;

        }

        private Direction GetDirection()
        {
            var ImSoRandom = new Random();
            int direction = ImSoRandom.Next(0, 7);
            return (Direction)direction;
        }
        private Point GetOriginPoint(Direction thisDirection)
        {
            Point InsertionPoint = GetRandomInsertionPoint(thisDirection, this._TargetWord.Length);
            return InsertionPoint;
        }

        public MatrixAndOrigin GenerateMatrix()
        {
            _myMatrix = new MatrixChar[this._MatrixSizeX, this._MatrixSizeY]; // {{'A','B','C','D'},{'E','F','G','H'},{'I','J','K','L'},{'M','N','O','P'}};
            char[] targetWordAsChars = this._TargetWord.ToCharArray();
            var myDirection = GetDirection();
            FillGrid();
            Point InsertionPoint = GetOriginPoint(myDirection);
            InsertWordIntoGrid(InsertionPoint, myDirection, this._TargetWord);
            int x = 0;
            int y = 0;
            Point currentPoint = new Point(x, y);
            char randomChar = '.';
            GetRandomCharWithReplacement charGetter = new GetRandomCharWithReplacement(this._TargetWord);

            while (IsMatrixFull() == false)
            {
                randomChar = charGetter.GetChar();

                if (!_myMatrix[currentPoint.X, currentPoint.Y].Charhistory.Contains(randomChar))
                {
                    _myMatrix[currentPoint.X, currentPoint.Y].Char = randomChar;
                    _myMatrix[currentPoint.X, currentPoint.Y].Charhistory += randomChar;                    
                    if (IsValidMatrix(this._TargetWord))
                    {
                        currentPoint = GetNextPoint(currentPoint);
                    }
                    else
                    {
                        _myMatrix[currentPoint.X, currentPoint.Y].Char = '.';                        
                        if (_myMatrix[currentPoint.X, currentPoint.Y].Charhistory.Count() == charGetter.UniqueCharCount())
                        {                            
                            _myMatrix[currentPoint.X, currentPoint.Y].Charhistory = "";
                            currentPoint = GetPreviousPoint(currentPoint);
                        }
                    }
                }
                else
                {
                    // if we've exhausted all possibilities
                    if (_myMatrix[currentPoint.X, currentPoint.Y].Charhistory.Count() == charGetter.UniqueCharCount())
                    {
                        // backtrack                            
                        _myMatrix[currentPoint.X, currentPoint.Y].Charhistory = "";
                        _myMatrix[currentPoint.X, currentPoint.Y].Char = '.';
                        currentPoint = GetPreviousPoint(currentPoint);
                        
                    }
                }
            }

            return new MatrixAndOrigin(_myMatrix, InsertionPoint);                 
        }

        private bool IsMatrixFull()
        {
            for (int y = 0; y < _myMatrix.GetUpperBound(1) + 1; y++)
            {
                for (int x = 0; x < _myMatrix.GetUpperBound(0) + 1; x++)
                {
                    if (_myMatrix[x, y].Char == '.')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void FillGrid()
        {
            var xBound = _myMatrix.GetUpperBound(0) + 1;
            var yBound = _myMatrix.GetUpperBound(1) + 1;

            for (int i = 0; i < xBound; i++)
            {
                for (int j = 0; j < yBound; j++)
                {
                    _myMatrix[i, j] = new MatrixChar('.', false);
                }
            }
        }

        private void InsertWordIntoGrid(Point origin, Direction wordDirection, string keyword)
        {
            char[] word = keyword.ToCharArray(); 

            int x = origin.X;
            int y = origin.Y;

            foreach (var item in word)
            {
                _myMatrix[x, y] = new MatrixChar(item,true);
                
                switch (wordDirection)
                {
                    case Direction.LeftToRight:
                        x += 1;
                        break;
                    case Direction.TopDown:
                        y += 1;
                        break;
                    case Direction.RightToLeft:
                        x -= 1;
                        break;
                    case Direction.BottomUp:
                        y -= 1;
                        break;
                    case Direction.DiagonalDownLeft:
                        x -= 1;
                        y += 1;
                        break;
                    case Direction.DiagonalDownRight:
                        x += 1;
                        y += 1;
                        break;
                    case Direction.DiagonalUpLeft:                        
                        x -= 1;
                        y -= 1;
                        break;
                    case Direction.DiagonalUpRight:
                        x += 1;
                        y -= 1;
                        break;
                    default:
                        break;
                }
            }
            //PrintGrid(myMatrix);
        }
        
        public static void PrintSolutionGrid(MatrixChar[,] myMatrix)
        {
            for (int i = 0; i <= myMatrix.GetUpperBound(1); i++)
            {
                for (int j = 0; j <= myMatrix.GetUpperBound(0); j++)
                {
                    if (myMatrix[j, i].Protected == true)
                    {
                        Console.Write(myMatrix[j, i].Char + " ");
                    }
                    else
                    {
                        Console.Write(". ");
                    }                    
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static void PrintGrid(MatrixChar[,] myMatrix)
        {
            for (int i = 0; i <= myMatrix.GetUpperBound(1); i++)
            {
                for (int j = 0; j <= myMatrix.GetUpperBound(0); j++)
                {
                    Console.Write(myMatrix[j, i].Char + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private Point GetNextPoint(Point current)
        {
            var x = current.X;
            var y = current.Y;
            do
            {
                x += 1;
                if (x == _myMatrix.GetUpperBound(0) + 1)
                {
                    x = 0;
                    y += 1;
                    if (y == _myMatrix.GetUpperBound(1) + 1)
                    {
                        y = 0;
                    }
                }

            } while (_myMatrix[x, y].Protected == true);

            return new Point(x, y);
        }
        private Point GetPreviousPoint(Point current)
        {
            var x = current.X;
            var y = current.Y;
            do
            {
                x -= 1;
                if (x < 0)
                {
                    x = _myMatrix.GetUpperBound(0);
                    y -= 1;
                    if (y < 0)
                    {
                        y = _myMatrix.GetUpperBound(1);
                    }
                }
            } while (_myMatrix[x, y].Protected == true);


            return new Point(x, y);
        }

        private Point GetRandomInsertionPoint(Direction wordDirection, int wordSize)
        {
            var matrixSizeX = _myMatrix.GetUpperBound(0) + 1;
            var matrixSizeY = _myMatrix.GetUpperBound(1) + 1;
            wordSize -= 1;
            var ImSoRandom = new Random();
            int x = 0;
            int y = 0;

            switch (wordDirection)
            {
                case Direction.LeftToRight:
                    x = ImSoRandom.Next(0, matrixSizeX - wordSize);
                    y = ImSoRandom.Next(0, matrixSizeY);
                    break;
                case Direction.TopDown:
                    x = ImSoRandom.Next(0, matrixSizeX);
                    y = ImSoRandom.Next(0, matrixSizeY - wordSize);
                    break;
                case Direction.RightToLeft:
                    x = ImSoRandom.Next(wordSize, matrixSizeX);
                    y = ImSoRandom.Next(0, matrixSizeY);
                    break;
                case Direction.BottomUp:
                    x = ImSoRandom.Next(0, matrixSizeX);
                    y = ImSoRandom.Next(wordSize, matrixSizeY);
                    break;
                case Direction.DiagonalUpRight:
                    x = ImSoRandom.Next(0, matrixSizeX - wordSize);
                    y = ImSoRandom.Next(wordSize, matrixSizeY);
                    break;
                case Direction.DiagonalDownRight:
                    x = ImSoRandom.Next(0, matrixSizeX - wordSize);
                    y = ImSoRandom.Next(0, matrixSizeY - wordSize);
                    break;
                case Direction.DiagonalDownLeft:
                    x = ImSoRandom.Next(wordSize, matrixSizeX);
                    y = ImSoRandom.Next(0, matrixSizeY - wordSize);
                    break;
                case Direction.DiagonalUpLeft:
                    x = ImSoRandom.Next(wordSize, matrixSizeX);
                    y = ImSoRandom.Next(wordSize, matrixSizeY);
                    break;
                default:
                    break;
            }

            return new Point(x, y);
        }
                
        private int MatchCount(string keyword)
        {
            var myString1 = keyword;
            var myString2 = Reverse(myString1);
            var extractedRanks = ExtractRanks(_myMatrix);
            var matches = extractedRanks.Where(i => i.Contains(myString1)).Count();
            matches += extractedRanks.Where(i => i.Contains(myString2)).Count();
            return matches;
        }

        public static int MatchCount(MatrixChar[,] thisMatrix, string keyword)
        {
            var myString1 = keyword;
            var myString2 = Reverse(myString1);
            var extractedRanks = ExtractRanks(thisMatrix);
            var matches = extractedRanks.Where(i => i.Contains(myString1)).Count();
            matches += extractedRanks.Where(i => i.Contains(myString2)).Count();
            return matches;
        }


        private  bool IsValidMatrix(string keyword)
        {
            var matchesCount = MatchCount(keyword);
            if (matchesCount == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidMatrix(MatrixChar[,] thisMatrix, string keyword)
        {
            var matchesCount = MatchCount(thisMatrix,keyword);
            if (matchesCount == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static List<string> ExtractRanks(MatrixChar[,] thisMatrix)
        {
            var xBound = thisMatrix.GetUpperBound(0) + 1;
            var yBound = thisMatrix.GetUpperBound(1) + 1;
            var result = new List<string>();

            string extract = "";

            for (int y = 0; y < yBound; y++)
            {
                extract = "";
                for (int x = 0; x < xBound; x++)
                {
                    extract = extract + thisMatrix[x, y].Char;
                }

                if (extract.Length > 0)
                {
                    result.Add(extract);
                }
            }

            for (int x = 0; x < xBound; x++)
            {
                extract = "";
                for (int y = 0; y < yBound; y++)
                {
                    extract = extract + thisMatrix[x, y].Char;
                }

                if (extract.Length > 0)
                {
                    result.Add(extract);
                }
            }

            // based upon indexing method found here
            // http://stackoverflow.com/questions/2112832/traverse-rectangular-matrix-in-diagonal-strips
            for (int slice = 0; slice < xBound + yBound - 1; ++slice)
            {
                extract = "";
                int z2 = slice < xBound ? 0 : slice - xBound + 1;
                int z1 = slice < yBound ? 0 : slice - yBound + 1;
                for (int j = slice - z2; j >= z1; --j)
                {
                    extract = extract + thisMatrix[j, slice - j].Char;
                }
                result.Add(extract);
            }

            for (int slice = 0; slice < xBound + yBound - 1; ++slice)
            {
                extract = "";
                int z2 = slice < xBound ? 0 : slice - xBound + 1;
                int z1 = slice < yBound ? 0 : slice - yBound + 1;
                for (int j = slice - z2; j >= z1; --j)
                {
                    extract = extract + thisMatrix[xBound - j - 1, slice - j].Char;
                    //extract = extract + thisMatrix[j,yBound - slice - j].Char;
                }
                result.Add(extract);
            }

            return result;

        }        

    }
}
