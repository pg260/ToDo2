using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using ToDo2.Domain.Contracts;
using ToDo2.Domain.Entities;

namespace ToDo2.Infra.Context;

public class BaseDbContext : DbContext, IUnitOfWork
{
    public DbSet<Users> Users { get; set; } = null!;
    public DbSet<Tasks> Tasks { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasCharSet("utf8mb4")
            .UseCollation("utf8mb4_0900_ai_ci")
            .UseGuidCollation(string.Empty);
        
        modelBuilder.Ignore<ValidationResult>();
        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> Commit() => await SaveChangesAsync() > 0;
}