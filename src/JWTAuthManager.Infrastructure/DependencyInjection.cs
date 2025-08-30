using JWTAuthManager.Application.Interfaces;
using JWTAuthManager.Application.Services;
using JWTAuthManager.Domain.Repositories;
using JWTAuthManager.Infrastructure.Data;
using JWTAuthManager.Infrastructure.Identy;
using JWTAuthManager.Infrastructure.Repositories;
using JWTAuthManager.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JWTAuthManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}