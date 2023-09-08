using Microsoft.EntityFrameworkCore;
using ToDo2.Domain.Contracts.Repositories;
using ToDo2.Domain.Entities;
using ToDo2.Infra.Context;

namespace ToDo2.Infra.Repositories;

public class TasksRepository : BaseRepository<Tasks>, ITaskRepositories
{
    public TasksRepository(BaseDbContext context) : base(context)
    {
    }

    public void Create(Tasks tasks)
    {
        Context.Tasks.Add(tasks);
    }

    public void Update(Tasks tasks)
    {
        Context.Tasks.Update(tasks);
    }

    public void Remove(Tasks tasks)
    {
        Context.Tasks.Remove(tasks);
    }

    public async Task<Tasks?> GetById(int id)
    {
        return await Context.Tasks
            .AsNoTracking()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
    }
}