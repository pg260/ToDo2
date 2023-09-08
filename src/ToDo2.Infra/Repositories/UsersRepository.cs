using Microsoft.EntityFrameworkCore;
using ToDo2.Domain.Contracts.Repositories;
using ToDo2.Domain.Entities;
using ToDo2.Infra.Context;

namespace ToDo2.Infra.Repositories;

public class UsersRepository : BaseRepository<Users>, IUsersRepository
{
    public UsersRepository(BaseDbContext context) : base(context)
    {
    }

    public void Create(Users users)
    {
        Context.Users.Add(users);
    }

    public void Update(Users users)
    {
        Context.Users.Update(users);
    }

    public void Remove(Users users)
    {
        Context.Users.Remove(users);
    }

    public async Task<Users?> GetById(int id)
    {
        return await Context.Users
            .AsNoTracking()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
    }
}