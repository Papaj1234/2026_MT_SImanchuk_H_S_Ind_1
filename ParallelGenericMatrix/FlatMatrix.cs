using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using ParallelGenericMatrix.Interfaces;

namespace ParallelGenericMatrix
{
    public sealed class FlatMatrix<T> : IMatrix<T> where T : INumber<T>
    {
        private readonly T[] data;

        public int Rows { get; }
        public int Cols { get; }

        public T this[int row, int col]
        {
            get => data[row * Cols + col];
            set => data[row * Cols + col] = value;
        }

        public FlatMatrix(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            data = new T[rows * cols];
        }

        public static IMatrix<T> LoadFromBinaresFile(string path)
        {
            using var reader = new BinaryReader(File.OpenRead(path));
            int rows = reader.ReadInt32();
            int cols = reader.ReadInt32();
            var matrix = new FlatMatrix<T>(rows, cols);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = ReadValue(reader);
                }
            }
            return matrix;
        }

        private static T ReadValue(BinaryReader reader)
        {
            if (typeof(T) == typeof(int))
            {
                return (T)(object)reader.ReadInt32();
            }
            if (typeof(T) == typeof(double))
            {
                return (T)(object)reader.ReadDouble();
            }
            if (typeof(T) == typeof(float))
            {
                return (T)(object)reader.ReadSingle();
            }
            if (typeof(T) == typeof(long))
            {
                return (T)(object)reader.ReadInt64();
            }
            throw new NotSupportedException($"Type {typeof(T)} is not supported.");
        }

        public IMatrix<T> AddByRowsSequential(IMatrix<T> other)
        {
            ArgumentNullException.ThrowIfNull(other);
            var result = new FlatMatrix<T>(Rows, Cols);
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    result[i, j] = this[i, j] + other[i, j];
                }
            }
            return result;
        }

        public IMatrix<T> AddByRowsParallel(IMatrix<T> other)
        {
            ArgumentNullException.ThrowIfNull(other);
            var result = new FlatMatrix<T>(Rows, Cols);
            Parallel.For(0, Rows, i =>
            {
                for (int j = 0; j < Cols; j++)
                {
                    result[i, j] = this[i, j] + other[i, j];
                }
            });
            return result;
        }

        public IMatrix<T> AddByColumnsSequential(IMatrix<T> other)
        {
            ArgumentNullException.ThrowIfNull(other);
            var result = new FlatMatrix<T>(Rows, Cols);
            for (int j = 0; j < Cols; j++)
            {
                for (int i = 0; i < Rows; i++)
                {
                    result[i, j] = this[i, j] + other[i, j];
                }
            }
            return result;
        }

        public IMatrix<T> AddByColumnsParallel(IMatrix<T> other)
        {
            ArgumentNullException.ThrowIfNull(other);
            var result = new FlatMatrix<T>(Rows, Cols);
            Parallel.For(0, Cols, j =>
            {
                for (int i = 0; i < Rows; i++)
                {
                    result[i, j] = this[i, j] + other[i, j];
                }
            });
            return result;
        }

        public IMatrix<T> MultiplySequential(IMatrix<T> other)
        {
            ArgumentNullException.ThrowIfNull(other);
            var result = new FlatMatrix<T>(Rows, other.Cols);
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < other.Cols; j++)
                {
                    for (int k = 0; k < Cols; k++)
                    {
                        result[i, j] += this[i, k] * other[k, j];
                    }
                }
            }
            return result;
        }

        public IMatrix<T> MultiplyParallel(IMatrix<T> other)
        {
            ArgumentNullException.ThrowIfNull(other);
            var result = new FlatMatrix<T>(Rows, other.Cols);
            Parallel.For(0, Rows, i =>
            {
                for (int j = 0; j < other.Cols; j++)
                {
                    for (int k = 0; k < Cols; k++)
                    {
                        result[i, j] += this[i, k] * other[k, j];
                    }
                }
            });
            return result;
        }

        public IMatrix<T> MultiplyOptimSequential(IMatrix<T> other)
        {
            ArgumentNullException.ThrowIfNull(other);
            var result = new FlatMatrix<T>(Rows, other.Cols);
            for (int i = 0; i < Rows; i++)
            {
                for (int k = 0; k < Cols; k++)
                {
                    for (int j = 0; j < other.Cols; j++)
                    {
                        result[i, j] += this[i, k] * other[k, j];
                    }
                }
            }
            return result;
        }

        public IMatrix<T> MultiplyOptimParallel(IMatrix<T> other)
        {
            ArgumentNullException.ThrowIfNull(other);
            var result = new FlatMatrix<T>(Rows, other.Cols);
            Parallel.For(0, Rows, i =>
            {
                for (int k = 0; k < Cols; k++)
                {
                    for (int j = 0; j < other.Cols; j++)
                    {
                        result[i, j] += this[i, k] * other[k, j];
                    }
                }
            });
            return result;
        }

        public IMatrix<T> MultiplyNaiveSequential(IMatrix<T> other)
        {
            ArgumentNullException.ThrowIfNull(other);
            var result = new FlatMatrix<T>(Rows, other.Cols);
            for (int j = 0; j < other.Cols; j++)
            {
                for (int i = 0; i < Rows; i++)
                {
                    for (int k = 0; k < Cols; k++)
                    {
                        result[i, j] += this[i, k] * other[k, j];
                    }
                }
            }
            return result;
        }

        public IMatrix<T> MultiplyNaiveParallel(IMatrix<T> other)
        {
            ArgumentNullException.ThrowIfNull(other);
            var result = new FlatMatrix<T>(Rows, other.Cols);
            Parallel.For(0, other.Cols, j =>
            {
                for (int i = 0; i < Rows; i++)
                {
                    for (int k = 0; k < Cols; k++)
                    {
                        result[i, j] += this[i, k] * other[k, j];
                    }
                }
            });
            return result;
        }
    }
}