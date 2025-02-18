using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastructure.Context;

namespace Restaurants.Infrastructure.Repositories;

internal class DishesRepo(RestaurantsDbContext restaurantsDbContext) : IDishesRepo
{
    public async Task<int> CreateDishAsync(Dish dish)
    {
        restaurantsDbContext.Dishes.Add(dish);
        await restaurantsDbContext.SaveChangesAsync();
        return dish.Id;
    }
}