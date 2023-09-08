using ToDo2.Services.Dtos.PaginatedSearch;
using ToDo2.Services.Dtos.Tasks;

namespace ToDo2.Services.Contracts;

public interface ITasksServices
{
    Task<TasksDto?> Create(AddTasksDto dto);
    Task<TasksDto?> Update(int id, UpdateTasksDto dto);
    Task Remove(int id);
    Task Concluir(int id);
    Task Desconcluir(int id);
    Task<TasksDto?> GetById(int id);
    Task<PagedDto<TasksDto>> Search(BuscarTasksDto dto);
}