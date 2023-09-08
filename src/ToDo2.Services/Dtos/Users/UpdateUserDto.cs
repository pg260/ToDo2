namespace ToDo2.Services.Dtos.Users;

public class UpdateUserDto
{
    public int id { get; set; }
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Senha { get; set; } = null!;
}