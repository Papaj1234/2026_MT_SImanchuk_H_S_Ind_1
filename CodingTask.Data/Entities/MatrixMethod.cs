namespace CodingTask.Data.Entities;

public enum MatrixMethod
{
    AddByRowsSequential,
    AddByRowsParallel,
    AddByColumnsSequential,
    AddByColumnsParallel,
    MultiplySequential,
    MultiplyParallel,
    MultiplyOptimSequential,
    MultiplyOptimParallel,
    MultiplyNaiveSequential,
    MultiplyNaiveParallel,
}