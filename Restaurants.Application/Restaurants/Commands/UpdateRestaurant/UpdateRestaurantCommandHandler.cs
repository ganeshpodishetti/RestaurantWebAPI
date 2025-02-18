using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger,
    IRestaurantsRepo restaurantsRepo, IMapper mapper) 
    : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Update Restaurant with Id: {Restaurant.Id} with {@UpdateRestaurantCommand}", request.Id, request);
        var restaurant = await restaurantsRepo.GetRestaurantByIdAsync(request.Id);
        if (restaurant == null) 
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        
        mapper.Map(request, restaurant);
        
        // restaurant.Name = request.Name;
        // restaurant.HasDelivery = request.HasDelivery;
        // restaurant.Description = request.Description;
        
        await restaurantsRepo.UpdateRestaurant();
    }
}