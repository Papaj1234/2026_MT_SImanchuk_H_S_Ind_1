using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixGenerator
{
    internal sealed class MatrixFileWriter
    {
        internal void Write(string path, double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            using var writer = new BinaryWriter(File.OpenWrite(path));
            writer.Write(rows);
            writer.Write(cols);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    writer.Write(matrix[i, j]);
                }
            }
        }
    }
}