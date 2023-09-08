using ToDo2.Services.Dtos.PaginatedSearch;

namespace ToDo2.Services.Dtos.Users;

public class BuscarUsersDto : PaginatedSearchDto<Domain.Entities.Users>
{
    public string? Nome { get; set; } = null;
    public string? Email { get; set; } = null;

    public override void ApplyFilters(ref IQueryable<Domain.Entities.Users> query)
    {
        if (Nome != null) query = query.Where(c => c.Nome.Contains(Nome));
        if (Email != null) query = query.Where(c => c.Email.Contains(Email));
    }

    public override void ApplyOrdenation(ref IQueryable<Domain.Entities.Users> query)
    {
        if (DirectionOfOrdenation.ToLower().Trim().Equals("desc"))
        {
            query = OrdenationBy.ToLower().Trim() switch
            {
                "nome" => query.OrderByDescending(c => c.Nome),
                "email" => query.OrderByDescending(c => c.Email),
                _ => query.OrderByDescending(c => c.Id)
            };
            return;
        }

        query = OrdenationBy.ToLower().Trim() switch
        {
            "nome" => query.OrderBy(c => c.Nome),
            "email" => query.OrderBy(c => c.Email),
            _ => query.OrderBy(c => c.Id)
        };
    }
}