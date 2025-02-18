using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
    IRestaurantsRepo restaurantsRepo,
    IDishesRepo dishesRepo,
    IMapper mapper) : IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new dish: {@DishRequest}", request);
        var restaurant = await restaurantsRepo.GetRestaurantByIdAsync(request.RestaurantId);
        if (restaurant == null) 
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        var dish = mapper.Map<Dish>(request);
        return await dishesRepo.CreateDishAsync(dish);
    }
}