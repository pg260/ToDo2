namespace ToDo2.Domain.Contracts;

public interface IUnitOfWork
{
    Task<bool> Commit();
}