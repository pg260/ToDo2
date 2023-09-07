using ToDo2.Domain.Entities;

namespace ToDo2.Domain.Contracts.Repositories;

public interface IUsersRepository : IBaseRepository<Users>
{
    void Create(Users users);
    void Edit(Users users);
    void Remove(Users users);
    void GetById(Users users);
}