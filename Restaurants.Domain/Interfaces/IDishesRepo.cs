using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Interfaces;

public interface IDishesRepo
{
    Task<int> CreateDishAsync(Dish dish);
}