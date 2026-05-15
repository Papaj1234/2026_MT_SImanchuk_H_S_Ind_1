using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixGenerator
{
    internal sealed class RandomMatrixSource : IMatrixSource
    {
        private readonly Random rng = new();

        public double[,] Generate(int rows, int cols)
        {
            var matrix = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = rng.NextDouble() * 100.0;
                }
            }
            return matrix;
        }
    }
}