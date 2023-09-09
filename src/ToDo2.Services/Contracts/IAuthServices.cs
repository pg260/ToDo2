using ToDo2.Services.Dtos.Auth;

namespace ToDo2.Services.Contracts;

public interface IAuthServices
{
    Task<AuthUserDto?> Login(LoginDto dto);
}