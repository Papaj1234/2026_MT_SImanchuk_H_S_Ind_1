using System;
using System.Collections.Generic;
using System.Linq;
using CodingTask;
using NUnit.Framework;

namespace CodingTask.Test;
public class BeginnersTest
{
    public static IEnumerable<object[]> GetJaggedMatrix()
    {
        yield return new object[]
        {
            1, new int[][] { new int[] { 1 }, },
        };

        yield return new object[]
        {
            2, new int[][] { new int[] { 1 }, new int[] { 1, 2 }, },
        };

        yield return new object[]
        {
            3, new int[][] { new int[] { 1 }, new int[] { 1, 2 }, new int[] { 1, 2, 3 }, },
        };

        yield return new object[]
        {
            4, new int[][] { new int[] { 1 }, new int[] { 1, 2 }, new int[] { 1, 2, 3 }, new int[] { 1, 2, 3, 4 }, },
        };
    }

    public static IEnumerable<int> ConvertMatrixJaggedToIEnumareble(int[][] matr)
    {
        List<int> result = new List<int>();
        ArgumentNullException.ThrowIfNull(matr);
        foreach (var record in matr)
        {
            result.AddRange(record.ToList());
        }

        return result;
    }

    [TestCase(1, 1, 2, 2)]
    [TestCase(1, 8, 8, 1)]
    [TestCase(6, 2, 2, 6)]
    [TestCase(2, 7, 7, 2)]
    [TestCase(3, 3, 6, 6)]
    public void ChessMasterBishopIsFightCorrect(byte a, byte b, byte c, byte d)
    {
        Assert.IsTrue(TaskClass.ChessMasterBishop(a, b, c, d));
    }

    [TestCase(1, 8, 1, 1)]
    [TestCase(1, 8, 2, 2)]
    [TestCase(8, 1, 8, 8)]
    [TestCase(7, 7, 7, 2)]
    [TestCase(3, 3, 6, 8)]
    public void ChessMasterBishopIsFightIncorrect(byte a, byte b, byte c, byte d)
    {
        Assert.IsFalse(TaskClass.ChessMasterBishop(a, b, c, d));
    }

    [TestCase(1, 1, 1, 8)]
    [TestCase(1, 8, 8, 8)]
    [TestCase(6, 2, 1, 2)]
    [TestCase(2, 7, 2, 1)]
    [TestCase(3, 3, 3, 8)]
    public void ChessMasterCastleIsFightCorrect(byte a, byte b, byte c, byte d)
    {
        Assert.IsTrue(TaskClass.ChessMasterCastle(a, b, c, d));
    }

    [TestCase(1, 8, 2, 7)]
    [TestCase(8, 8, 2, 2)]
    [TestCase(8, 1, 5, 5)]
    [TestCase(7, 7, 1, 2)]
    [TestCase(3, 3, 6, 8)]
    public void ChessMasterCastleIsFightIncorrect(byte a, byte b, byte c, byte d)
    {
        Assert.IsFalse(TaskClass.ChessMasterCastle(a, b, c, d));
    }

    [TestCase(145345, 4, 2)]
    [TestCase(145345, 5, 2)]
    [TestCase(878888, 8, 5)]
    [TestCase(75757, 7, 3)]
    [TestCase(313, 1, 1)]
    public void CountDigitOccuresInNumberReturnsCorrectTrue(int number, int digit, int expected)
    {
        int actual = TaskClass.CountDigitOccuresInNumber(number, digit);
        Assert.AreEqual(expected, actual, $"In {number} digit {digit} ocurres {actual} expected {expected}");
    }

    [TestCaseSource(nameof(GetJaggedMatrix))]
    public void JaggedArrayReturnsCorrectTrue(int n, int[][] expected)
    {
        var actual = ConvertMatrixJaggedToIEnumareble(TaskClass.JaggedArray(n));
        var exp2 = ConvertMatrixJaggedToIEnumareble(expected);
        Console.WriteLine(string.Join(';', actual));
        Console.WriteLine(string.Join(';', exp2));
        Assert.IsTrue(exp2.SequenceEqual(actual), "Jagged array was created incorectly!");
    }

    [TestCase(new int[] { 7, 3, 5 }, new int[] { 3, 5, 7 })]
    [TestCase(new int[] { 7, 6, 5, 4, 3, 2, 1, 0 }, new int[] { 0, 1, 2, 3, 4, 5, 6, 7 })]
    [TestCase(new int[] { 10, -10, 8, -6, 6, -3, 3, -1, 0 }, new int[] { -10, -6, -3, -1, 0, 3, 6, 8, 10 })]
    public void SortArrayCorrectResultTrue(int[] array, int[] expected)
    {
        var res = TaskClass.SortArray(array);
        Assert.IsTrue(res.SequenceEqual(expected), "Input array was sorted incorectly!");
    }
}