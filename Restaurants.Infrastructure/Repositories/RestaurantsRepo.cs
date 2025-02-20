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
    
    public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingRestaurantsAsync(string? searchPhrase, int pageSize, int pageNumber)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        
        // var restaurants = await restaurantsDbContext.Restaurants
        //     .Where(r => r.Name.ToLower().Contains(searchPhraseLower) 
        //         || r.Description.ToLower().Contains(searchPhraseLower))
        //     .ToListAsync();

        //Checking directly on database
        var baseQuery = restaurantsDbContext
            .Restaurants
            .Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower)
                                                      || r.Description.ToLower().Contains(searchPhraseLower)));
        
        
         var totalCount = await baseQuery.CountAsync();
         
         var restaurants = await baseQuery
             .Skip(pageSize * (pageNumber -1))
             .Take(pageSize)
             .ToListAsync();
        
        return (restaurants, totalCount);
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
        restaurantsDbContext.Add(restaurant);
        await restaurantsDbContext.SaveChangesAsync();
        return restaurant.Id;
    }

    public async Task DeleteRestaurant(Restaurant restaurant)
    {
        restaurantsDbContext.Remove(restaurant);
        await restaurantsDbContext.SaveChangesAsync();
    }

    public Task UpdateRestaurant() 
        => restaurantsDbContext.SaveChangesAsync();
}