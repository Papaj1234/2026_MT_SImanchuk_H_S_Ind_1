using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ParallelGenericMatrix
{
    public interface IMatrix<T> where T : INumber<T>
    {
        int Rows { get; }
        int Cols { get; }
        T this[int row, int col] { get; set; }
        static abstract IMatrix<T> LoadFromBinaresFile(string path);
        IMatrix<T> AddByRowsSequential(IMatrix<T> other);
        IMatrix<T> AddByRowsParallel(IMatrix<T> other);
        IMatrix<T> AddByColumnsSequential(IMatrix<T> other);
        IMatrix<T> AddByColumnsParallel(IMatrix<T> other);
        IMatrix<T> MultiplySequential(IMatrix<T> other); // (i-j-k)
        IMatrix<T> MultiplyParallel(IMatrix<T> other); // (i-j-k)
        IMatrix<T> MultiplyOptimSequential(IMatrix<T> other); // (i-k-j)
        IMatrix<T> MultiplyOptimParallel(IMatrix<T> other); // (i-k-j)
        IMatrix<T> MultiplyNaiveSequential(IMatrix<T> other); // (j-i-k)
        IMatrix<T> MultiplyNaiveParallel(IMatrix<T> other); // (j-i-k)
    }
}
