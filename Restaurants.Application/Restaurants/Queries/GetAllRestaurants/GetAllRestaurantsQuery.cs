using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.Dtos;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQuery : IRequest<PagedResult<RestaurantDto>>
{
    public string? searchPhrase { get; set; }
    public int pageNumber { get; set; }
    public int pageSize { get; set; }
    public string? SortBy { get; set; }
    public Sort SortOrder { get; set; }
}