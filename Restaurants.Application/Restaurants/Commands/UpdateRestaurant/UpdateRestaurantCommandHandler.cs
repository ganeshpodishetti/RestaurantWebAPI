using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger,
    IRestaurantsRepo restaurantsRepo, IMapper mapper) 
    : IRequestHandler<UpdateRestaurantCommand, bool>
{
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Update Restaurant with Id: {Restaurant.Id} with {@UpdateRestaurantCommand}", request.Id, request);
        var restaurant = await restaurantsRepo.GetRestaurantByIdAsync(request.Id);
        if (restaurant == null) return false;
        
        mapper.Map(request, restaurant);
        
        // restaurant.Name = request.Name;
        // restaurant.HasDelivery = request.HasDelivery;
        // restaurant.Description = request.Description;
        
        await restaurantsRepo.UpdateRestaurant(restaurant);
        return true;
    }
}