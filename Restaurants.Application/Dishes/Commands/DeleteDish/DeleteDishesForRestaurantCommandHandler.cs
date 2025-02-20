using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Dishes.Commands.DeleteDish;

public class DeleteDishesForRestaurantCommandHandler(ILogger<DeleteDishesForRestaurantCommand> logger,
    IRestaurantsRepo restaurantsRepo,
    IDishesRepo dishesRepo, IRestaurantAuthorizationService restaurantAuthorizationService,
    IMapper mapper) : IRequestHandler<DeleteDishesForRestaurantCommand>
{
    public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Removing all dishes for restaurant with Id: {RestaurantId}", request.RestaurantId);
        
        var restaurant = await restaurantsRepo.GetRestaurantByIdAsync(request.RestaurantId);
        if(restaurant is null)
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        
        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
            throw new ForbidException();


        await dishesRepo.DeleteDish(restaurant.Dishes);
    }
}