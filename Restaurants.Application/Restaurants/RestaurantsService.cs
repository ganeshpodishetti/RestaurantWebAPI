using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService(IRestaurantsRepo restaurantsRepo, ILogger<RestaurantsService> logger)
: IRestaurantsService
{
    public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
    {
        logger.LogInformation("Getting all restaurants");
        var restaurants = await restaurantsRepo.GetAllRestaurantsAsync();
        return restaurants;
    }
    
}

