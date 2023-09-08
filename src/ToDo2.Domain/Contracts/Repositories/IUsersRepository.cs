using ToDo2.Domain.Entities;

namespace ToDo2.Domain.Contracts.Repositories;

public interface IUsersRepository : IBaseRepository<Users>
{
    void Create(Users users);
    void Update(Users users);
    void Remove(Users users);
    Task<Users?> GetById(int id);
}