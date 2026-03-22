using System.Text;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using LibraryManagementSystem.Core.Interfaces.Services;
using LibraryManagementSystem.Infrastructure.Persistence;
using LibraryManagementSystem.Infrastructure.Repositories;
using LibraryManagementSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace LibraryManagementSystem.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddMediatR(Assembly.GetExecutingAssembly());
        // services.AddAutoMapper(Assembly.GetExecutingAssembly()); (later)
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgreSQLConnection")));
        
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        // JWT
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });
        return services;
    }
}