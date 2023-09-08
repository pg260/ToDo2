using System.Linq.Expressions;
using ToDo2.Domain.Contracts.PaginatedSearch;
using ToDo2.Domain.Entities;

namespace ToDo2.Domain.Contracts.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    public IUnitOfWork UnitOfWork { get; }
    Task<IPaginatedResult<T>> Search(IPaginatedSearch<T> filtro);
    Task<T?> FirstOrDefault(Expression<Func<T, bool>> predicate);
    Task<bool> Any(Expression<Func<T, bool>> predicate);
}