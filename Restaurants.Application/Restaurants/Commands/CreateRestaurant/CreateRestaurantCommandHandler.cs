using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger,
    IMapper mapper,
    IRestaurantsRepo restaurantsRepo) : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new restaurant");
        var restaurant = mapper.Map<Restaurant>(request);
        var restaurantId = await restaurantsRepo.CreateRestaurantAsync(restaurant);
        return restaurantId;
    }
}