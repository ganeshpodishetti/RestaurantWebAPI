using MediatR;
using Restaurants.Application.Dtos;

namespace Restaurants.Application.Dishes.Queries.GetDishesByIdForRestaurant;

public class GetDishesByIdForRestaurantQuery(int restaurantId, int dishId) : IRequest<DishDto>
{
    public int RestaurantId { get; } = restaurantId;
    public int DishId { get; } = dishId;
}