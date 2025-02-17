using Microsoft.Extensions.Logging;
using Restaurants.Application.Dtos;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService(IRestaurantsRepo restaurantsRepo, ILogger<RestaurantsService> logger)
: IRestaurantsService
{
    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync()
    {
        logger.LogInformation("Getting all restaurants");
        var restaurants = await restaurantsRepo.GetAllRestaurantsAsync();

        var restaurantDtos = restaurants.Select(RestaurantDto.FromEntity);
        return restaurantDtos!;
    }

    public async Task<RestaurantDto?> GetRestaurantByIdAsync(int id)
    {
        logger.LogInformation($"Getting restaurant with id: {id}");
        var restaurant = await restaurantsRepo.GetRestaurantByIdAsync(id);
        var restaurantDtos = RestaurantDto.FromEntity(restaurant);
        return restaurantDtos;
    }
}

