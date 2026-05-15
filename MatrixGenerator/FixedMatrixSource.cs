using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixGenerator
{
    internal sealed class FixedMatrixSource : IMatrixSource
    {
        private readonly double[,] fixedData;

        internal FixedMatrixSource(double[,] data)
        {
            fixedData = data;
        }

        public double[,] Generate(int rows, int cols) => fixedData;
    }
}