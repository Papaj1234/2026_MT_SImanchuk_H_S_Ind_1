using System.IO;
using ParallelGenericMatrix;

namespace MatrixBenchmark
{
    internal static class MatrixLoader
    {
        internal static (IMatrix<double> a, IMatrix<double> b) Load<TMatrix>(string dir, string name)
            where TMatrix : IMatrix<double>
        {
            var a = TMatrix.LoadFromBinaresFile(Path.Combine(dir, $"{name}_A.bin"));
            var b = TMatrix.LoadFromBinaresFile(Path.Combine(dir, $"{name}_B.bin"));
            return (a, b);
        }
    }
}