using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearchLib
{
    public struct MatrixAndOrigin
    {
        public MatrixChar[,] Matrix;
        public Point Origin;
        
        public MatrixAndOrigin(MatrixChar[,] matrix, Point origin)
        {
            Matrix = matrix;
            Origin = origin;            
        }
    }
}
