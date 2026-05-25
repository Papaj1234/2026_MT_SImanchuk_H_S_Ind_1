using CodingTask.Data.Entities;

namespace CodingTask.Data.Repositories.Interfaces;

public interface IHttpScanRunRepository : IRepository<HttpScanRun>
{
    Task<IEnumerable<HttpScanRun>> GetByOperationTypeAsync(OperationType operationType);
}