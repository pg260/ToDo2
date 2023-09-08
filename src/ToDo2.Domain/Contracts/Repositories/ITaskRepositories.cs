using ToDo2.Domain.Entities;

namespace ToDo2.Domain.Contracts.Repositories;

public interface ITaskRepositories : IBaseRepository<Tasks>
{
    void Create(Tasks tasks);
    void Update(Tasks tasks);
    void Remove(Tasks tasks);
    Task<Tasks?> GetById(int id);
}