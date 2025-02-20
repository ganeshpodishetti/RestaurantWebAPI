using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandQueryHandler(ILogger<DeleteRestaurantCommandQuery> logger,
    IRestaurantsRepo restaurantsRepo,
    IRestaurantAuthorizationService restaurantAuthorizationService) 
    : IRequestHandler<DeleteRestaurantCommandQuery>
{
    public async Task Handle(DeleteRestaurantCommandQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Delete Restaurant with Id: {Restaurant.Id}", request.Id);
        var restaurant = await restaurantsRepo.GetRestaurantByIdAsync(request.Id);
        
        if (restaurant == null) 
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
            throw new ForbidException();
        
        await restaurantsRepo.DeleteRestaurant(restaurant);
    }
}