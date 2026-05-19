using System;

namespace MatrixGenerator
{
    internal sealed class Program
    {
        static void Main(string[] args)
        {
            string outputDir = args.Length > 0 ? args[0] : "matrices";
            MatrixGeneratorRunner.Run(outputDir);
        }
    }
}