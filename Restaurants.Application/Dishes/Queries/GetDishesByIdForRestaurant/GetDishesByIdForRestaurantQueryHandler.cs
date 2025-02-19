using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Dishes.Queries.GetDishesByIdForRestaurant;

public class GetDishesByIdForRestaurantQueryHandler(ILogger<GetDishesByIdForRestaurantQueryHandler> logger,
    IMapper mapper,
    IRestaurantsRepo restaurantsRepo)
    : IRequestHandler<GetDishesByIdForRestaurantQuery, DishDto>
{
    public async Task<DishDto> Handle(GetDishesByIdForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving Dishes with Id: {DishId} For Restaurant with Id:{RestaurantId}", request.DishId, request.RestaurantId);
        var restaurant = await restaurantsRepo.GetRestaurantByIdAsync(request.RestaurantId);
        if (restaurant is null)
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);
        if (dish is null)
            throw new NotFoundException(nameof(Dish), request.DishId.ToString());
        var dishes = mapper.Map<DishDto>(dish);
        return dishes;
    }
}