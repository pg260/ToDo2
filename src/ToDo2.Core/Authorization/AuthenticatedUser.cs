using Microsoft.AspNetCore.Http;
using ToDo2.Core.Extensions;

namespace ToDo2.Core.Authorization;

public class AuthenticatedUser : IAuthenticatedUser
{
    public AuthenticatedUser()
    { }

    public AuthenticatedUser(IHttpContextAccessor httpContextAccessor)
    {
        Id = httpContextAccessor.ObterUsuarioId()!.Value;
        Nome = httpContextAccessor.ObterUsuarioNome();
        Email = httpContextAccessor.ObterUsuarioEmail();
    }
    
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
}