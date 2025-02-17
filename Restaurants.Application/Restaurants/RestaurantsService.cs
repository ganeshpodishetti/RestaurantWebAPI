using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService(IRestaurantsRepo restaurantsRepo, ILogger<RestaurantsService> logger, IMapper mapper)
: IRestaurantsService
{
    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync()
    {
        logger.LogInformation("Getting all restaurants");
        var restaurants = await restaurantsRepo.GetAllRestaurantsAsync();
        var restaurantDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        return restaurantDtos!;
    }

    public async Task<RestaurantDto?> GetRestaurantByIdAsync(int id)
    {
        logger.LogInformation($"Getting restaurant with id: {id}");
        var restaurant = await restaurantsRepo.GetRestaurantByIdAsync(id);
        var restaurantDtos = mapper.Map<RestaurantDto>(restaurant);
        return restaurantDtos;
    }

    public async Task<int> CreateRestaurantAsync(CreateRestaurantDto createRestaurantDto)
    {
        logger.LogInformation("Creating a new restaurant");
        var restaurant = mapper.Map<Restaurant>(createRestaurantDto);
        var restaurantId = await restaurantsRepo.CreateRestaurantAsync(restaurant);
        return restaurantId;
    }
}

