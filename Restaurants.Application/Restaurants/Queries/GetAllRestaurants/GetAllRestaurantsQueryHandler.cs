using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Dtos;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger,
    IRestaurantsRepo restaurantsRepo, IMapper mapper) : 
    IRequestHandler<GetAllRestaurantsQuery, PagedResult<RestaurantDto>>
{
    public async Task<PagedResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        // Other approach -> if searchPhrase is not null
        // if (request.searchPhrase != null)
        // {
        //     var searchPhraseLower = request.searchPhrase.ToLower();
        //     logger.LogInformation("Getting all restaurants");
        //     var restaurantsMatch = await restaurantsRepo.GetAllMatchingRestaurantsAsync(searchPhraseLower);
        //     var restaurantMatchDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurantsMatch);
        //     return restaurantMatchDtos!;
        // }
        
        // Bad filtering approach
        // var restaurants = (await restaurantsRepo.GetAllRestaurantsAsync())
        //     .Where(r => r.Name.ToLower().Contains(searchPhraseLower) 
        //     || r.Description.ToLower().Contains(searchPhraseLower));
        
        logger.LogInformation("Getting all restaurants");
        var (restaurants, totalCount) = await restaurantsRepo.GetAllMatchingRestaurantsAsync(request.searchPhrase,
            request.pageSize, request.pageNumber, request.SortBy, request.SortOrder);
        
        var restaurantDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        var result = new PagedResult<RestaurantDto>(restaurantDtos, totalCount, request.pageSize, request.pageNumber);
        return result;
    }
}