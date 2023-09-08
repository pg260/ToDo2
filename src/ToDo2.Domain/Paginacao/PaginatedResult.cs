using ToDo2.Domain.Contracts.PaginatedSearch;

namespace ToDo2.Domain.Paginacao;

public class PaginatedResult<T> : IPaginatedResult<T>
{
    public PaginatedResult()
    {
        Pagination = new Pagination();
        Items = new List<T>();
    }

    public PaginatedResult(int page, int total, int perPages) : this()
    {
        Pagination = new Pagination
        {
            PageNumber = page,
            TotalPages = total,
            TotalInPage = perPages
        };
    }
    
    public IList<T> Items { get; set; }
    public IPagination Pagination { get; set; }
}