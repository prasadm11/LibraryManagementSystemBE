using System.Reflection;
using MediatR;
using AutoMapper;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using LibraryManagementSystem.Core.Interfaces.Services;
using LibraryManagementSystem.Infrastructure.Services;
// using LibraryManagementSystem.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
namespace LibraryManagementSystem.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));
        services.AddScoped<INotificationService, NotificationService>();
        
        return services;
    }
}