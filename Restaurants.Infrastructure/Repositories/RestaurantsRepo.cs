using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastructure.Context;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantsRepo(RestaurantsDbContext restaurantsDbContext) : IRestaurantsRepo
{
    public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
    {
        var restaurants = await restaurantsDbContext.Restaurants.ToListAsync();
        return restaurants;
    }
}