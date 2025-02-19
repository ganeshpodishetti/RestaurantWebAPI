using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Restaurants.Application.Users;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtension).Assembly;
        
        // MediatR 
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        
        // Auto Mapper
        services.AddAutoMapper(applicationAssembly);
        
        // Fluent Validation
        services.AddValidatorsFromAssembly(applicationAssembly).AddFluentValidationAutoValidation();
        
        // IUser Context
        services.AddScoped<IUserContext, UserContext>();
        
        services.AddHttpContextAccessor();
    }
}