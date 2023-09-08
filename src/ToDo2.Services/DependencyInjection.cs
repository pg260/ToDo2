using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDo2.Infra;
using ToDo2.Services.Contracts;
using ToDo2.Services.MapperConfig;
using ToDo2.Services.NotificatorConfig;
using ToDo2.Services.Services;

namespace ToDo2.Services;

public static class DependencyInjection
{
    public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);
        
        services.AddRepositories();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITasksServices, TasksServices>();
        services.AddScoped<IUsersServices, UsersServices>();
        services.AddScoped<INotificator, Notificator>();
    }

    public static void CreateAutomapper(this IServiceCollection services)
    {
        var provider = CreateServiceProvider();
        
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.ConstructServicesUsing(provider.GetService);
            mc.AddProfile(new AutoMapperProfile());
        });
        
        mappingConfig.CreateMapper();
    }
    
    private static ServiceProvider CreateServiceProvider()
    {
        var services = new ServiceCollection();
        return services.BuildServiceProvider();
    }
    
}