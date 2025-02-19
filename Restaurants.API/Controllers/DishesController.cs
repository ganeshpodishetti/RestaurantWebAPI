using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDish;
using Restaurants.Application.Dishes.Queries.GetDishesByIdForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;
using Restaurants.Application.Dtos;

namespace Restaurants.API.Controllers;

[Route("api/restaurants/{restaurantId}/dishes")]
[ApiController]
public class DishesController(IMediator mediator) : ControllerBase
{
    // POST
    [HttpPost]
    public async Task<IActionResult> CreateDishAsync([FromRoute]int restaurantId, CreateDishCommand command)
    {
        command.RestaurantId = restaurantId;
        var dishId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetDishesByIdForRestaurants), new {restaurantId, dishId}, null);
    }
    
    // GET
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DishDto>>> GetDishesForRestaurants([FromRoute]int restaurantId)
    {
        var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));
        return Ok(dishes);
    }
    
    [HttpGet("{dishId}")]
    public async Task<ActionResult<DishDto>> GetDishesByIdForRestaurants([FromRoute]int restaurantId, [FromRoute]int dishId)
    {
        var dishes = await mediator.Send(new GetDishesByIdForRestaurantQuery(restaurantId, dishId));
        return Ok(dishes);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteDish([FromRoute] int restaurantId)
    {
        await mediator.Send(new DeleteDishesForRestaurantCommand(restaurantId));
        return NoContent();
    }
}