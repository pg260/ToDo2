using ToDo2.Services.Dtos.PaginatedSearch;

namespace ToDo2.Services.Dtos.Tasks;

public class BuscarTasksDto : PaginatedSearchDto<Domain.Entities.Tasks>
{
    public string? Nome { get; set; } = null;
    public string? Descricao { get; set; } = null;
    public bool? Concluido { get; set; } = null;
    public DateTime? CriadoEm { get; set; } = null;
    public DateTime? DataExpiracao { get; set; } = null;

    public override void ApplyFilters(ref IQueryable<Domain.Entities.Tasks> query)
    {
        if (Nome != null) query = query.Where(c => c.Nome.ToLower().Contains(Nome.Trim().ToLower()));
        if (Descricao != null) query = query.Where(c => c.Descricao != null && c.Descricao.ToLower().Contains(Descricao.Trim().ToLower()));
        if (Concluido != null) query = query.Where(c => c.Concluido == Concluido);
        if (CriadoEm != null) query = query.Where(c => c.CriadoEm >= CriadoEm);
        if (DataExpiracao != null) query = query.Where(c => c.DataExpiracao >= DataExpiracao);
    }

    public override void ApplyOrdenation(ref IQueryable<Domain.Entities.Tasks> query)
    {
        if (DirectionOfOrdenation.Trim().ToLower().Equals("desc"))
        {
            query = OrdenationBy.ToLower().Trim() switch
            {
                "nome" => query.OrderByDescending(c => c.Nome),
                "descricao" => query.OrderByDescending(c => c.Descricao),
                "concluido" => query.OrderByDescending(c => c.Concluido),
                "CriadoEm" => query.OrderByDescending(c => c.CriadoEm),
                "DataExpiracao" => query.OrderByDescending(c => c.DataExpiracao),
                _ => query.OrderByDescending(c => c.Id)
            };
            
            return;
        }
        
        query = OrdenationBy.ToLower().Trim() switch
        {
            "nome" => query.OrderBy(c => c.Nome),
            "descricao" => query.OrderBy(c => c.Descricao),
            "concluido" => query.OrderBy(c => c.Concluido),
            "CriadoEm" => query.OrderBy(c => c.CriadoEm),
            "DataExpiracao" => query.OrderBy(c => c.DataExpiracao),
            _ => query.OrderBy(c => c.Id)
        };
    }
}