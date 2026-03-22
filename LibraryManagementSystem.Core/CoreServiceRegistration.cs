namespace LibraryManagementSystem.Core;
using Microsoft.Extensions.DependencyInjection;
public static class CoreServiceRegistration
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        // services.AddMediatR(Assembly.GetExecutingAssembly());
        // services.AddAutoMapper(Assembly.GetExecutingAssembly()); (later)

        return services;
    }
}