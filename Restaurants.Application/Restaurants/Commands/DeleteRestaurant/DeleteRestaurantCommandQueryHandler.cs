using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandQueryHandler(ILogger<DeleteRestaurantCommandQuery> logger,
    IRestaurantsRepo restaurantsRepo) 
    : IRequestHandler<DeleteRestaurantCommandQuery, bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommandQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Delete Restaurant with Id: {Restaurant.Id}", request.Id);
        var restaurant = await restaurantsRepo.GetRestaurantByIdAsync(request.Id);
        if (restaurant == null) return false;
        await restaurantsRepo.DeleteRestaurant(restaurant);
        return true;
    }
}