﻿using ToDo2.Domain.Contracts.PaginatedSearch;

namespace ToDo2.Services.Dtos.PaginatedSearch;

public class PaginationDto : IPagination
{
    public int TotalItems { get; set; }
    public int TotalInPage { get; set; }
    public int PageNumber { get; set; }
    public int CapacityItems { get; set; }
    public int TotalPages { get; set; }
}