using MediatR;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandQuery(int id) : IRequest
{
    public int Id { get; } = id;
}