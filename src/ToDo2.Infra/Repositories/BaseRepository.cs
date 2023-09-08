using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ToDo2.Domain.Contracts;
using ToDo2.Domain.Contracts.PaginatedSearch;
using ToDo2.Domain.Contracts.Repositories;
using ToDo2.Domain.Entities;
using ToDo2.Domain.Paginacao;
using ToDo2.Infra.Context;

namespace ToDo2.Infra.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected BaseRepository(BaseDbContext context)
    {
        Context = context;
        _dbSet = context.Set<T>();
    }
    
    private readonly DbSet<T> _dbSet;
    protected readonly BaseDbContext Context;

    public IUnitOfWork UnitOfWork => Context;
    
    public async Task<IPaginatedResult<T>> Search(IPaginatedSearch<T> filtro)
    {
        var queryable = _dbSet.AsQueryable();
        
        filtro.ApplyFilters(ref queryable);
        filtro.ApplyOrdenation(ref queryable);

        var resultado = new PaginatedResult<T>(filtro.Pages, filtro.PerPages, await queryable.CountAsync());

        var quantPages = (double)resultado.Pagination.TotalPages / filtro.PerPages;
        resultado.Pagination.TotalPages = (int)Math.Ceiling(quantPages);

        var skip = (filtro.Pages - 1) * filtro.PerPages;
        resultado.Items = await queryable.Skip(skip).Take(filtro.PerPages).ToListAsync();
        resultado.Pagination.TotalInPage = resultado.Items.Count;
        return resultado;
    }

    public async Task<T?> FirstOrDefault(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AsNoTrackingWithIdentityResolution().Where(predicate).FirstOrDefaultAsync();
    }

    public async Task<bool> Any(Expression<Func<T, bool>> predicate) => await _dbSet.AnyAsync(predicate);
}