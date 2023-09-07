using Microsoft.EntityFrameworkCore;
using ToDo2.Domain.Contracts;
using ToDo2.Domain.Entities;

namespace ToDo2.Infra.Context;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext, IUnitOfWork
{
    public DbSet<Users> Users { get; set; } = null!;
    public DbSet<Tasks> Tasks { get; set; } = null!;

    public async Task<bool> Commit() => await SaveChangesAsync() > 0;
}