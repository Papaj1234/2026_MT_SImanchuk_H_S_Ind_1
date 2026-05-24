using System;
using System.Diagnostics;
using ParallelGenericMatrix;

namespace MatrixBenchmark
{
    internal static class BreakEvenAnalyzer
    {
        internal static void Analyze()
        {
            int[] sizes = { 10, 50, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
            Console.WriteLine("=== Breakeven AddByRows ===");
            foreach (int n in sizes)
            {
                var a = new RectMatrix<double>(n, n);
                var b = new RectMatrix<double>(n, n);

                var sw1 = Stopwatch.StartNew();
                a.AddByRowsSequential(b);
                sw1.Stop();

                var sw2 = Stopwatch.StartNew();
                a.AddByRowsParallel(b);
                sw2.Stop();

                Console.WriteLine($"N={n}: Sequential={sw1.ElapsedMilliseconds}ms Parallel={sw2.ElapsedMilliseconds}ms");
            }
        }
    }
}