namespace ToDo2.Domain.Entities;

public class Users : BaseEntity
{
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Senha { get; set; } = null!;
    public DateTime CriadoEm { get; set; }

    public virtual List<Tasks> TasksList { get; set; } = new();
}