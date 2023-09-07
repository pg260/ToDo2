using ToDo2.Domain.Contracts.PaginatedSearch;
using ToDo2.Domain.Entities;

namespace ToDo2.Domain.Contracts.Repositories;

public interface IBaseRepository<T> : IDisposable where T : BaseEntity
{
    Task<IPaginatedResult<T>> Search(IPaginatedSearch<T> filtro);
}