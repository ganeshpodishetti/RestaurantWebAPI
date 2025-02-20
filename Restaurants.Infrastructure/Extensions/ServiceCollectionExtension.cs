using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastructure.Authorization.Requirements;
using Restaurants.Infrastructure.Authorization.Services;
using Restaurants.Infrastructure.Context;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RestaurantDbConnection");
        services.AddDbContext<RestaurantsDbContext>(options => options.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging());

        // Registering the Identity Services
        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<RestaurantsDbContext>();

        services.AddScoped<ISeeder, Seeder>();
        services.AddScoped<IRestaurantsRepo, RestaurantsRepo>();
        services.AddScoped<IDishesRepo, DishesRepo>();
        
        // Claim based access control
        services.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.HasNationality, policy => 
                policy.RequireClaim(AppClaimTypes.Nationality, "Indian", "American"))
            // custom authorization
            .AddPolicy(PolicyNames.HasAtLeast20,
                policy => policy.AddRequirements(new MinimumAgeRequirement(20)))
            .AddPolicy(PolicyNames.CreatedAtLeast2Restaurants,
                policy => policy.AddRequirements(new CreatedMultipleRestaurantsRequirement(2)));
        
        // Authorization handler
        services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
        services.AddScoped<IAuthorizationHandler, CreatedMultipleRestaurantsRequirementHandler>();
        
        // Custom Authorization
        services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
    }
}