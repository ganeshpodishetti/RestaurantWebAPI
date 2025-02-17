using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dtos;
using Restaurants.Application.Restaurants;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(IRestaurantsService restaurantsService) : ControllerBase
{
    // GET
    [HttpGet]
    public async Task<IActionResult> GetAllRestaurantsAsync()
    {
        var restaurants = await restaurantsService.GetAllRestaurantsAsync();
        return Ok(restaurants);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetRestaurantByIdAsync([FromRoute]int id)
    {
        var restaurants = await restaurantsService.GetRestaurantByIdAsync(id);
        if(restaurants is null) return NotFound();
        return Ok(restaurants);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurantAsync([FromBody]CreateRestaurantDto request)
    {
        var id = await restaurantsService.CreateRestaurantAsync(request);
        return CreatedAtAction(nameof(GetRestaurantByIdAsync), new { id }, null);
    }

    
}