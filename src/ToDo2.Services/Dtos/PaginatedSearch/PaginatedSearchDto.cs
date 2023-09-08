using ToDo2.Domain.Contracts.PaginatedSearch;
using ToDo2.Domain.Entities;

namespace ToDo2.Services.Dtos.PaginatedSearch;

public abstract class PaginatedSearchDto<T> : IPaginatedSearch<T> where T : BaseEntity
{
    public int Pages { get; set; } = 1;
    public int PerPages { get; set; } = 10;
    public string OrdenationBy { get; set; } = "id";
    public string DirectionOfOrdenation { get; set; } = "asc";

    public virtual void ApplyFilters(ref IQueryable<T> query)
    { }

    public virtual void ApplyOrdenation(ref IQueryable<T> query)
    { }
    
}