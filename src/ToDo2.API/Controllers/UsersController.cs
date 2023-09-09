using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDo2.API.Reponses;
using ToDo2.Services.Contracts;
using ToDo2.Services.Dtos.PaginatedSearch;
using ToDo2.Services.Dtos.Users;
using ToDo2.Services.NotificatorConfig;

namespace ToDo2.API.Controllers;

[Route("v1/Users")]
public class UsersController : BaseController
{
    public UsersController(INotificator notificator, IUsersServices usersServices) : base(notificator)
    {
        _usersServices = usersServices;
    }
    
    private readonly IUsersServices _usersServices;

    [HttpPost]
    [SwaggerOperation(Summary = "Cria um usuário.", Tags = new[] { "Users" })]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Create([FromBody] AddUsersDto dto)
    {
        var user = await _usersServices.Create(dto);
        return CreatedResponse(string.Empty, user);
    }

    [HttpPut("{id}")]
    [Authorize]
    [SwaggerOperation(Summary = "Editar um usuário.", Tags = new[] { "Users" })]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
    {
        var user = await _usersServices.Update(id, dto);
        return OkResponse(user);
    }

    [HttpDelete("{id}")]
    [Authorize]
    [SwaggerOperation(Summary = "Excluir um usuário.", Tags = new[] { "Users" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remove(int id)
    {
        await _usersServices.Remove(id);
        return NoContentResponse();
    }

    [HttpGet("{id}")]
    [Authorize]
    [SwaggerOperation(Summary = "Pegar um usuário por id.", Tags = new[] { "Users" })]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _usersServices.GetById(id);
        return OkResponse(user);
    }

    [HttpGet]
    [Authorize]
    [SwaggerOperation(Summary = "Buscar uma lista de usuários.", Tags = new[] { "Users" })]
    [ProducesResponseType(typeof(PagedDto<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Search([FromQuery] BuscarUsersDto dto)
    {
        var users = await _usersServices.Search(dto);
        return OkResponse(users);
    }
}