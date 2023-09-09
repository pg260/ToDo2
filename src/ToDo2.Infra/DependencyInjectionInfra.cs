﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDo2.Core.Authorization;
using ToDo2.Core.Extensions;
using ToDo2.Domain.Contracts.Repositories;
using ToDo2.Infra.Context;
using ToDo2.Infra.Repositories;

namespace ToDo2.Infra;

public static class DependencyInjectionInfra
{
    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthenticatedUser>(sp =>
        {
            var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
            return httpContextAccessor.UsuarioAutenticado() ? new AuthenticatedUser(httpContextAccessor) : new AuthenticatedUser();
        });
        
        services.AddDbContext<BaseDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            options.UseMySql(connectionString, serverVersion);
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITaskRepositories, TasksRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
    }

    public static void UseMigrations(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<BaseDbContext>();
        db.Database.Migrate();
    }
}