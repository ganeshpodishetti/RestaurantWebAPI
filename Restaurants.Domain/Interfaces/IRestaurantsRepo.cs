using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Interfaces;

public interface IRestaurantsRepo
{
    Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
}