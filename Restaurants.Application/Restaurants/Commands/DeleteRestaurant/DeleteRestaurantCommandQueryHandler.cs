using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandQueryHandler(ILogger<DeleteRestaurantCommandQuery> logger,
    IRestaurantsRepo restaurantsRepo) 
    : IRequestHandler<DeleteRestaurantCommandQuery>
{
    public async Task Handle(DeleteRestaurantCommandQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Delete Restaurant with Id: {Restaurant.Id}", request.Id);
        var restaurant = await restaurantsRepo.GetRestaurantByIdAsync(request.Id);
        if (restaurant == null) 
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        await restaurantsRepo.DeleteRestaurant(restaurant);
    }
}