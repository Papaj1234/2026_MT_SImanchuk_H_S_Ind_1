using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixGenerator
{
    public sealed class FixedMatrixSource : IMatrixSource
    {
        private readonly double[,] fixedData;

        public FixedMatrixSource(double[,] data)
        {
            fixedData = data;
        }

        public double[,] Generate(int rows, int cols) => fixedData;
    }
}