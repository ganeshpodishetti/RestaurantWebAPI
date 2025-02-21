using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Interfaces;

public interface IRestaurantsRepo
{
    Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
    Task<Restaurant?> GetRestaurantByIdAsync(int id);
    Task<int> CreateRestaurantAsync(Restaurant restaurant);
    Task DeleteRestaurant(Restaurant restaurant);
    Task UpdateRestaurant();
    Task<(IEnumerable<Restaurant>, int)> GetAllMatchingRestaurantsAsync(string searchPhrase, int pageSize, int pageNumber, 
        string? sortBy, Sort sortOrder);
}