using JWTAuthManager.Application.Common.Interfaces.Services;
using JWTAuthManager.Domain.Entities;
using JWTAuthManager.Domain.Interfaces.Repositories;
using JWTAuthManager.Infrastructure.Data;
using JWTAuthManager.Infrastructure.Repositories;
using JWTAuthManager.Infrastructure.Security;
using JWTAuthManager.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
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

        services.AddHttpContextAccessor();

        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IUnityOfWork, UnityOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IPasswordHasher<User>, BCryptPasswordHasher<User>>();

        return services;
    }
}