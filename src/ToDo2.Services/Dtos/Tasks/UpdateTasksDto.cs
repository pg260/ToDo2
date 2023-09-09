namespace ToDo2.Services.Dtos.Tasks;

public class UpdateTasksDto
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public DateTime? DataExpiracao { get; set; }
}