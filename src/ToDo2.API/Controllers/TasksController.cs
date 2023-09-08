using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDo2.API.Reponses;
using ToDo2.Services.Contracts;
using ToDo2.Services.Dtos.PaginatedSearch;
using ToDo2.Services.Dtos.Tasks;
using ToDo2.Services.NotificatorConfig;

namespace ToDo2.API.Controllers;

[Route("v1/Tasks")]
public class TasksController : BaseController
{
    public TasksController(INotificator notificator, ITasksServices tasksServices) : base(notificator)
    {
        _tasksServices = tasksServices;
    }

    private readonly ITasksServices _tasksServices;

    [HttpPost]
    [SwaggerOperation(Summary = "Criar uma task.", Tags = new[] { "Tasks" })]
    [ProducesResponseType(typeof(TasksDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Create([FromBody] AddTasksDto dto)
    {
        var task = await _tasksServices.Create(dto);
        return CreatedResponse(string.Empty, task);
    }
    
[HttpPut("{id}")]
    [SwaggerOperation(Summary = "Editar uma task.", Tags = new[] { "Tasks" })]
    [ProducesResponseType(typeof(TasksDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTasksDto dto)
    {
        var task = await _tasksServices.Update(id, dto);
        return OkResponse(task);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Excluir uma task.", Tags = new[] { "Tasks" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remove(int id)
    {
        await _tasksServices.Remove(id);
        return NoContentResponse();
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Pegar uma task por id.", Tags = new[] { "Tasks" })]
    [ProducesResponseType(typeof(TasksDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _tasksServices.GetById(id);
        return OkResponse(task);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Buscar uma lista de tasks", Tags = new[] { "Tasks" })]
    [ProducesResponseType(typeof(PagedDto<TasksDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Search([FromQuery] BuscarTasksDto dto)
    {
        var tasks = await _tasksServices.Search(dto);
        return OkResponse(tasks);
    }
    
}