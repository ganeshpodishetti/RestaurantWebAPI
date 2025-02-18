using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    // GET
    [HttpGet]
    public async Task<IActionResult> GetAllRestaurantsAsync()
    {
        var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
        return Ok(restaurants);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetRestaurantByIdAsync([FromRoute]int id)
    {
        var restaurants = await mediator.Send(new GetRestaurantByIdQuery(id));
        if(restaurants is null) return NotFound();
        return Ok(restaurants);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurantAsync([FromBody]CreateRestaurantCommand request)
    {
        var id = await mediator.Send(request);
        return CreatedAtAction(nameof(GetRestaurantByIdAsync), new { id }, null);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRestaurantByIdAsync([FromRoute] int id)
    {
        var isDeleted = await mediator.Send(new DeleteRestaurantCommandQuery(id));
        if(isDeleted) return NoContent();   
        return NotFound();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateRestaurantAsync([FromRoute] int id, UpdateRestaurantCommand request)
    {
        request.Id = id;
        var isUpdated = await mediator.Send(request);
        if(isUpdated) return NoContent();
        return NotFound();
    }
}