using ParallelGenericMatrix;
using System;
using System.Diagnostics;
using System.IO;

namespace MatrixBenchmark
{
    internal sealed class Program
    {
        static void Main(string[] args)
        {
            string matrixDir = args.Length > 0 ? args[0] : "matrices";

            Console.WriteLine("500x500");
            var a500 = RectMatrix<double>.LoadFromBinaresFile(Path.Combine(matrixDir, "matrix_500x500_A.bin"));
            var b500 = RectMatrix<double>.LoadFromBinaresFile(Path.Combine(matrixDir, "matrix_500x500_B.bin"));
            RunBenchmark("RectMatrix 500x500", a500, b500);

            Console.WriteLine("2000x2000");
            var a2000 = RectMatrix<double>.LoadFromBinaresFile(Path.Combine(matrixDir, "matrix_2000x2000_A.bin"));
            var b2000 = RectMatrix<double>.LoadFromBinaresFile(Path.Combine(matrixDir, "matrix_2000x2000_B.bin"));
            RunBenchmark("RectMatrix 2000x2000", a2000, b2000);
        }

        private static void RunBenchmark(string label, IMatrix<double> a, IMatrix<double> b)
        {
            Measure(label, "AddByRowsSequential", () => a.AddByRowsSequential(b));
            Measure(label, "AddByRowsParallel", () => a.AddByRowsParallel(b));
            Measure(label, "AddByColumnsSequential", () => a.AddByColumnsSequential(b));
            Measure(label, "AddByColumnsParallel", () => a.AddByColumnsParallel(b));
            Measure(label, "MultiplySequential", () => a.MultiplySequential(b));
            Measure(label, "MultiplyParallel", () => a.MultiplyParallel(b));
            Measure(label, "MultiplyOptimSequential", () => a.MultiplyOptimSequential(b));
            Measure(label, "MultiplyOptimParallel", () => a.MultiplyOptimParallel(b));
            Measure(label, "MultiplyNaiveSequential", () => a.MultiplyNaiveSequential(b));
            Measure(label, "MultiplyNaiveParallel", () => a.MultiplyNaiveParallel(b));
            Console.WriteLine();
        }

        private static void Measure(string label, string method, Action action)
        {
            var sw = Stopwatch.StartNew();
            action();
            sw.Stop();
            Console.WriteLine($"{label} | {method}: {sw.ElapsedMilliseconds} ms");
        }
    }
}
