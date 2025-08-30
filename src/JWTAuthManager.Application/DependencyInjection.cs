using FluentValidation;
using JWTAuthManager.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JWTAuthManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}