using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dtos;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger,
    IMapper mapper, IRestaurantsRepo restaurantsRepo) : 
    IRequestHandler<GetRestaurantByIdQuery, RestaurantDto?>
{
    public async Task<RestaurantDto?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting restaurant with id: {Restaurant.Id}", request.Id);
        var restaurant = await restaurantsRepo.GetRestaurantByIdAsync(request.Id);
        var restaurantDtos = mapper.Map<RestaurantDto?>(restaurant);
        return restaurantDtos;
    }
}