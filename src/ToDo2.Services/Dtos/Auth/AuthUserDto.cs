using ToDo2.Services.Dtos.Users;

namespace ToDo2.Services.Dtos.Auth;

public class AuthUserDto
{
    public UserDto UserDto { get; set; } = new();
    public string Token { get; set; } = null!;
}