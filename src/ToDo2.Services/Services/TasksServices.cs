using AutoMapper;
using ToDo2.Core.Authorization;
using ToDo2.Domain.Contracts.Repositories;
using ToDo2.Domain.Entities;
using ToDo2.Services.Contracts;
using ToDo2.Services.Dtos.PaginatedSearch;
using ToDo2.Services.Dtos.Tasks;
using ToDo2.Services.NotificatorConfig;

namespace ToDo2.Services.Services;

public class TasksServices : BaseService, ITasksServices
{
    public TasksServices(IMapper mapper, INotificator notificator, ITaskRepositories taskRepositories, IAuthenticatedUser authenticatedUser) : base(mapper, notificator)
    {
        _taskRepositories = taskRepositories;
        _authenticatedUser = authenticatedUser;
    }

    private readonly ITaskRepositories _taskRepositories;
    private readonly IAuthenticatedUser _authenticatedUser;

    public async Task<TasksDto?> Create(AddTasksDto dto)
    {
        var task = Mapper.Map<Tasks>(dto);
        if (!await Validate(task)) return null;
        
        task = CompletandoTasks(task);
        _taskRepositories.Create(task);
        if (await CommitChanges()) return Mapper.Map<TasksDto>(task);
        
        Notificator.Handle("Não foi possível criar a task.");
        return null;
    }

    public async Task<TasksDto?> Update(int id, UpdateTasksDto dto)
    {
        if (id != dto.Id)
        {
            Notificator.Handle("Os ids não conferem.");
            return null;
        }

        var task = await _taskRepositories.GetById(dto.Id);
        if (task == null || task.UserId != _authenticatedUser.Id)
        {
            Notificator.HandleNotFound();
            return null;
        }

        Mapper.Map(dto, task);
        if (!await Validate(task)) return null;

        _taskRepositories.Update(task);
        if (await CommitChanges()) return Mapper.Map<TasksDto>(task);
        
        Notificator.Handle("Não foi possível editar a task.");
        return null;
    }

    public async Task Remove(int id)
    {
        var oldTask = await _taskRepositories.GetById(id);
        if (oldTask == null || oldTask.UserId != _authenticatedUser.Id)
        {
            Notificator.HandleNotFound();
            return;
        }
        
        _taskRepositories.Remove(oldTask);
        if(!await CommitChanges()) Notificator.Handle("Não foi possível excluir a task.");
    }

    public async Task Concluir(int id)
    {
        var oldTask = await _taskRepositories.GetById(id);
        if (oldTask == null || oldTask.UserId != _authenticatedUser.Id)
        {
            Notificator.HandleNotFound();
            return;
        }

        oldTask.Concluido = true;
        _taskRepositories.Update(oldTask);
        if (!await CommitChanges()) Notificator.Handle("Não foi possível editar a task.");
    }

    public async Task Desconcluir(int id)
    {
        var oldTask = await _taskRepositories.GetById(id);
        if (oldTask == null || oldTask.UserId != _authenticatedUser.Id)
        {
            Notificator.HandleNotFound();
            return;
        }

        oldTask.Concluido = false;
        _taskRepositories.Update(oldTask);
        if (!await CommitChanges()) Notificator.Handle("Não foi possível editar a task.");
    }

    public async Task<TasksDto?> GetById(int id)
    {
        var oldTask = await _taskRepositories.GetById(id);
        if (oldTask != null && oldTask.UserId == _authenticatedUser.Id) return Mapper.Map<TasksDto>(oldTask);
            
        Notificator.HandleNotFound();
        return null;
    }

    public async Task<PagedDto<TasksDto>> Search(BuscarTasksDto dto)
    {
        dto.UserId = _authenticatedUser.Id;
        var tasks = await _taskRepositories.Search(dto);
        return Mapper.Map<PagedDto<TasksDto>>(tasks);
    }

    private async Task<bool> Validate(Tasks tasks)
    {
        if (!tasks.Validar(out var validationResult))
        {
            Notificator.Handle(validationResult.Errors);
            return false;
        }

        if (!await _taskRepositories.Any(c => c.Id != tasks.Id && c.Nome == tasks.Nome && c.UserId == _authenticatedUser.Id)) return true;
        
        Notificator.Handle("Já existe uma task com esse mesmo nome.");
        return false;
    }

    private async Task<bool> CommitChanges() => await _taskRepositories.UnitOfWork.Commit();

    private Tasks CompletandoTasks(Tasks task, bool criando = true)
    {
        task.AtualizadoEm = DateTime.Now;
        if (!criando) return task;
        
        task.UserId = _authenticatedUser.Id;
        task.Concluido = false;
        task.CriadoEm = DateTime.Now;
        return task;
    }
}