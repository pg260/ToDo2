using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDo2.API.Reponses;
using ToDo2.Services.Contracts;
using ToDo2.Services.Dtos.Auth;
using ToDo2.Services.NotificatorConfig;

namespace ToDo2.API.Controllers;

[Route("v1/Auth")]
public class AuthController : BaseController
{
    public AuthController(INotificator notificator, IAuthServices authServices) : base(notificator)
    {
        _authServices = authServices;
    }

    private readonly IAuthServices _authServices;
    
    [HttpPost]
    [SwaggerOperation(Summary = "Logar", Tags = new[] { "Auth" })]
    [ProducesResponseType(typeof(AuthUserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var token = await _authServices.Login(dto);
        return CreatedResponse(string.Empty, token);
    }
}