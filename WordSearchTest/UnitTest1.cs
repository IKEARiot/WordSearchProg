using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordSearchLib;
using System.Collections.Generic;
using System.Linq;


namespace WordSearchTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var myGenerator = new WordSearchGenerator("dog", 3, 3);
            WordSearchGenerator.PrintGrid(myGenerator.GenerateMatrix().Matrix);

            myGenerator = new WordSearchGenerator("dog", 3, 6);
            WordSearchGenerator.PrintGrid(myGenerator.GenerateMatrix().Matrix);

            myGenerator = new WordSearchGenerator("dog", 6, 3);
            WordSearchGenerator.PrintGrid(myGenerator.GenerateMatrix().Matrix);

            myGenerator = new WordSearchGenerator("dog", 10, 10);
            WordSearchGenerator.PrintGrid(myGenerator.GenerateMatrix().Matrix);

            myGenerator = new WordSearchGenerator("dog", 10, 20);
            WordSearchGenerator.PrintGrid(myGenerator.GenerateMatrix().Matrix);

            myGenerator = new WordSearchGenerator("dog", 20, 10);
            WordSearchGenerator.PrintGrid(myGenerator.GenerateMatrix().Matrix);            

        }

        [TestMethod]
        public void TestMethod3()
        {
            WordSearchGenerator myGenerator;
            MatrixChar[,] result;
            myGenerator = new WordSearchGenerator("dog", 8, 7);
            result = myGenerator.GenerateMatrix().Matrix;
            WordSearchGenerator.PrintGrid(result);
            WordSearchGenerator.PrintSolutionGrid(result);
            foreach (var item in WordSearchGenerator.ExtractRanks(result))
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(WordSearchGenerator.MatchCount(result,"dog"));
        }

        [TestMethod]
        public void TestMethod4()
        {

            var myString1 = "dog";
            var myString2 = "god";
            var extractedRanks =  new List<string>() {"dgodogo"};  //ExtractRanks(thisMatrix);
           // var matches = extractedRanks.Where(i => i.Contains(myString1) || i.Contains(myString2));
            var matches = extractedRanks.Where(i => i.Contains(myString1)).Count();
            matches += extractedRanks.Where(i => i.Contains(myString2)).Count();

            Assert.IsTrue(matches == 2);
        }

        [TestMethod]
        public void TestMethod2()
        {

            int i = 0;
            WordSearchGenerator myGenerator;
            MatrixChar[,] result;

            myGenerator = new WordSearchGenerator("dog", 3, 3);
            for (i = 0; i < 100; i++)
            {                
                result = myGenerator.GenerateMatrix().Matrix;                
                if (WordSearchGenerator.IsValidMatrix(result,"dog") == false)
                {
                    WordSearchGenerator.PrintGrid(result);
                    Assert.Fail();
                }
            }

            myGenerator = new WordSearchGenerator("dog", 3, 6);
            for (i = 0; i < 100; i++)
            {
                result = myGenerator.GenerateMatrix().Matrix;                
                if (WordSearchGenerator.IsValidMatrix(result, "dog") == false)
                {
                    WordSearchGenerator.PrintGrid(result);
                    Assert.Fail();
                }
            }

            myGenerator = new WordSearchGenerator("dog", 6, 3);
            for (i = 0; i < 100; i++)
            {
                result = myGenerator.GenerateMatrix().Matrix;                
                if (WordSearchGenerator.IsValidMatrix(result, "dog") == false)
                {
                    WordSearchGenerator.PrintGrid(result);
                    Assert.Fail();
                }
            }

            myGenerator = new WordSearchGenerator("dog", 10, 10);
            for (i = 0; i < 100; i++)
            {
                result = myGenerator.GenerateMatrix().Matrix;                
                if (WordSearchGenerator.IsValidMatrix(result, "dog") == false)
                {                    
                    WordSearchGenerator.PrintGrid(result);
                    Assert.Fail();
                }
            }

            myGenerator = new WordSearchGenerator("dog", 10, 20);
            for (i = 0; i < 100; i++)
            {
                result = myGenerator.GenerateMatrix().Matrix;                
                if (WordSearchGenerator.IsValidMatrix(result, "dog") == false)
                {
                    WordSearchGenerator.PrintGrid(result);
                    Assert.Fail();
                }
            }

            myGenerator = new WordSearchGenerator("dog", 20, 10);
            for (i = 0; i < 100; i++)
            {
                result = myGenerator.GenerateMatrix().Matrix;                
                if (WordSearchGenerator.IsValidMatrix(result, "dog") == false)
                {
                    WordSearchGenerator.PrintGrid(result);
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public void TestMethod5()
        {
            WordSearchGenerator myGenerator;
            MatrixChar[,] result;
            myGenerator = new WordSearchGenerator("gattaca", 14, 14);
            result = myGenerator.GenerateMatrix().Matrix;
            WordSearchGenerator.PrintGrid(result);
        }


        [TestMethod]
        public void TestMethod6()
        {
            WordSearchGenerator myGenerator;
            MatrixChar[,] result;
            myGenerator = new WordSearchGenerator("gattaca", 100, 100);
            result = myGenerator.GenerateMatrix().Matrix;
            WordSearchGenerator.PrintGrid(result);
        }

    }
}
