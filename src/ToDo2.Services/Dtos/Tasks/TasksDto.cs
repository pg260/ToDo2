namespace ToDo2.Services.Dtos.Tasks;

public class TasksDto
{
    public string Nome { get; set; } = null!;
    public string? Descricao { get; set; }
    public bool Concluida { get; set; }
    public DateTime? DataExpiracao { get; set; }
    public DateTime CriadoEm { get; set; }
}