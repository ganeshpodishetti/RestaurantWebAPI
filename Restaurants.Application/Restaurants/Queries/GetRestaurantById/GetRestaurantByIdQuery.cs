using MediatR;
using Restaurants.Application.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQuery(int id) : IRequest<RestaurantDto>
{
    // public GetRestaurantByIdQuery(int id)
    // {
    //     Id = id;
    // }
    public int Id { get; } = id;
}