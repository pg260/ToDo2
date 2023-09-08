using ToDo2.Domain.Contracts.PaginatedSearch;

namespace ToDo2.Services.Dtos.PaginatedSearch;

public class PagedDto<T> : IPaginatedResult<T>
{
    public PagedDto(IList<T> items, IPagination pagination)
    {
        Items = new List<T>();
        Pagination = new PaginationDto();
    }

    public IList<T> Items { get; set; }
    public IPagination Pagination { get; set; }
}