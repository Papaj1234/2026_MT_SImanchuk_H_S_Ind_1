using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ParallelGenericMatrix;

namespace CodingTask.Test;

public class MatrixTests
{
    private static readonly double[][] A = new double[][]
    {
        new double[] { 1,  2,  3,  4,  5,  6,  7,  8,  9, 10 },
        new double[] { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 },
        new double[] { 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 },
        new double[] { 31, 32, 33, 34, 35, 36, 37, 38, 39, 40 },
        new double[] { 41, 42, 43, 44, 45, 46, 47, 48, 49, 50 },
        new double[] { 51, 52, 53, 54, 55, 56, 57, 58, 59, 60 },
        new double[] { 61, 62, 63, 64, 65, 66, 67, 68, 69, 70 },
        new double[] { 71, 72, 73, 74, 75, 76, 77, 78, 79, 80 },
        new double[] { 81, 82, 83, 84, 85, 86, 87, 88, 89, 90 },
        new double[] { 91, 92, 93, 94, 95, 96, 97, 98, 99, 100 },
    };

    [Test]
    public void MultiplyAllMethodsGiveSameResultRectMatrix()
    {
        var m = CreateRect(A);
        var seq = ToArray(m.MultiplySequential(m));
        var par = ToArray(m.MultiplyParallel(m));
        var optSeq = ToArray(m.MultiplyOptimSequential(m));
        var optPar = ToArray(m.MultiplyOptimParallel(m));
        var navSeq = ToArray(m.MultiplyNaiveSequential(m));
        var navPar = ToArray(m.MultiplyNaiveParallel(m));

        Assert.That(par, Is.EqualTo(seq).Within(1e-9));
        Assert.That(optSeq, Is.EqualTo(seq).Within(1e-9));
        Assert.That(optPar, Is.EqualTo(seq).Within(1e-9));
        Assert.That(navSeq, Is.EqualTo(seq).Within(1e-9));
        Assert.That(navPar, Is.EqualTo(seq).Within(1e-9));
    }

    [Test]
    public void MultiplyAllMethodsGiveSameResultJaggedMatrix()
    {
        var rect = CreateRect(A);
        var m = new JaggedMatrix<double>(10, 10);
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                m[i, j] = rect[i, j];
            }
        }

        var seq = ToArray(m.MultiplySequential(m));
        var par = ToArray(m.MultiplyParallel(m));
        var optSeq = ToArray(m.MultiplyOptimSequential(m));
        var optPar = ToArray(m.MultiplyOptimParallel(m));
        var navSeq = ToArray(m.MultiplyNaiveSequential(m));
        var navPar = ToArray(m.MultiplyNaiveParallel(m));

        Assert.That(par, Is.EqualTo(seq).Within(1e-9));
        Assert.That(optSeq, Is.EqualTo(seq).Within(1e-9));
        Assert.That(optPar, Is.EqualTo(seq).Within(1e-9));
        Assert.That(navSeq, Is.EqualTo(seq).Within(1e-9));
        Assert.That(navPar, Is.EqualTo(seq).Within(1e-9));
    }

    [Test]
    public void MultiplyAllMethodsGiveSameResultFlatMatrix()
    {
        var rect = CreateRect(A);
        var m = new FlatMatrix<double>(10, 10);
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                m[i, j] = rect[i, j];
            }
        }

        var seq = ToArray(m.MultiplySequential(m));
        var par = ToArray(m.MultiplyParallel(m));
        var optSeq = ToArray(m.MultiplyOptimSequential(m));
        var optPar = ToArray(m.MultiplyOptimParallel(m));
        var navSeq = ToArray(m.MultiplyNaiveSequential(m));
        var navPar = ToArray(m.MultiplyNaiveParallel(m));

        Assert.That(par, Is.EqualTo(seq).Within(1e-9));
        Assert.That(optSeq, Is.EqualTo(seq).Within(1e-9));
        Assert.That(optPar, Is.EqualTo(seq).Within(1e-9));
        Assert.That(navSeq, Is.EqualTo(seq).Within(1e-9));
        Assert.That(navPar, Is.EqualTo(seq).Within(1e-9));
    }

    private static RectMatrix<double> CreateRect(double[][] data)
    {
        int rows = data.Length;
        int cols = data[0].Length;
        var m = new RectMatrix<double>(rows, cols);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                m[i, j] = data[i][j];
            }
        }

        return m;
    }

    private static double[][] ToArray(IMatrix<double> m)
    {
        var result = new double[m.Rows][];
        for (int i = 0; i < m.Rows; i++)
        {
            result[i] = new double[m.Cols];
            for (int j = 0; j < m.Cols; j++)
            {
                result[i][j] = m[i, j];
            }
        }

        return result;
    }
}