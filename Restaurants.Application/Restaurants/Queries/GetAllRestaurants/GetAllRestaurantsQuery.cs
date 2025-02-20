using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQuery : IRequest<PagedResult<RestaurantDto>>
{
    public string? searchPhrase { get; set; }
    public int pageNumber { get; set; }
    public int pageSize { get; set; }
}