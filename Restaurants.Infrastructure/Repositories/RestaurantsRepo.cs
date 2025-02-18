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

    public async Task<Restaurant?> GetRestaurantByIdAsync(int id)
    {
        var restaurants = await restaurantsDbContext.Restaurants
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(x => x.Id == id);
        return restaurants;
    }

    public async Task<int> CreateRestaurantAsync(Restaurant restaurant)
    {
        restaurantsDbContext.Restaurants.Add(restaurant);
        await restaurantsDbContext.SaveChangesAsync();
        return restaurant.Id;
    }

    public async Task DeleteRestaurant(Restaurant restaurant)
    {
        restaurantsDbContext.Restaurants.Remove(restaurant);
        await restaurantsDbContext.SaveChangesAsync();
    }

    public async Task UpdateRestaurant(Restaurant restaurant)
    {
        restaurantsDbContext.Restaurants.Update(restaurant);
        await restaurantsDbContext.SaveChangesAsync();
    }
}