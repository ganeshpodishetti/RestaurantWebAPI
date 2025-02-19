using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesForRestaurantQueryHandler(ILogger<GetDishesForRestaurantQueryHandler> logger, 
    IMapper mapper, IRestaurantsRepo restaurantsRepo) 
    : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
{
    public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving all dishes for restaurant with id: {RestaurantId}", request.RestaurantId);
        var restaurant = await restaurantsRepo.GetRestaurantByIdAsync(request.RestaurantId);
        if(restaurant is null)
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        var result = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);
        return result;
    }
}