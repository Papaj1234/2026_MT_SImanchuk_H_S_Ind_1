using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixGenerator
{
    internal sealed class MatrixGeneratorRunner
    {
        private readonly MatrixFileWriter writer = new();
        private readonly RandomMatrixSource random = new();

        internal void Run(string outputDir)
        {
            Directory.CreateDirectory(outputDir);
            var fixed10 = new FixedMatrixSource(Get10x10());

            GeneratePair(outputDir, "matrix_10x10", 10, 10, fixed10);
            GeneratePair(outputDir, "matrix_500x500", 500, 500, random);
            GeneratePair(outputDir, "matrix_2000x2000", 2000, 2000, random);

            Console.WriteLine("Done: " + Path.GetFullPath(outputDir));
        }

        private void GeneratePair(string dir, string name, int rows, int cols, IMatrixSource source)
        {
            writer.Write(Path.Combine(dir, $"{name}_A.bin"), source.Generate(rows, cols));
            writer.Write(Path.Combine(dir, $"{name}_B.bin"), source.Generate(rows, cols));
            Console.WriteLine($"Generated {name}");
        }

        private static double[,] Get10x10() => new double[,]
        {
            {  1,  2,  3,  4,  5,  6,  7,  8,  9, 10 },
            { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 },
            { 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 },
            { 31, 32, 33, 34, 35, 36, 37, 38, 39, 40 },
            { 41, 42, 43, 44, 45, 46, 47, 48, 49, 50 },
            { 51, 52, 53, 54, 55, 56, 57, 58, 59, 60 },
            { 61, 62, 63, 64, 65, 66, 67, 68, 69, 70 },
            { 71, 72, 73, 74, 75, 76, 77, 78, 79, 80 },
            { 81, 82, 83, 84, 85, 86, 87, 88, 89, 90 },
            { 91, 92, 93, 94, 95, 96, 97, 98, 99, 100 },
        };
    }
}