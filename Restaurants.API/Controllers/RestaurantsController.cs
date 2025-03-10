using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Domain.Constants;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants")]
[Authorize]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    // GET
    [HttpGet]
    [AllowAnonymous]
    //[Authorize(Policy = PolicyNames.CreatedAtLeast2Restaurants)]
    public async Task<IActionResult> GetAllRestaurantsAsync([FromQuery]GetAllRestaurantsQuery query)
    {
        var restaurants = await mediator.Send(query);
        return Ok(restaurants);
    }

    [HttpGet("{id:int}")]
    [Authorize(Policy = PolicyNames.HasNationality)]
    public async Task<IActionResult> GetRestaurantByIdAsync([FromRoute]int id)
    {
        var restaurants = await mediator.Send(new GetRestaurantByIdQuery(id));
        return Ok(restaurants);
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> CreateRestaurantAsync(CreateRestaurantCommand request)
    {
        var id = await mediator.Send(request);
        return CreatedAtAction(nameof(GetRestaurantByIdAsync), new { id }, null);
    }
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRestaurantByIdAsync([FromRoute] int id)
    {
        await mediator.Send(new DeleteRestaurantCommandQuery(id)); 
        return NoContent();
    }
    
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRestaurantAsync([FromRoute] int id, UpdateRestaurantCommand request)
    {
        request.Id = id;
        await mediator.Send(request);
        return NoContent();
    }
    
    // [HttpPost("{id}/logo")]
    // public async Task<IActionResult> UploadLogo([FromRoute]int id, IFormFile file)
    // {
    //     await using var stream = file.OpenReadStream();
    //
    //     var command = new UploadRestaurantLogoCommand()
    //     {
    //         RestaurantId = id,
    //         FileName = $"{id}-{file.FileName}",
    //         File = stream
    //     };
    //
    //     await mediator.Send(command);
    //     return NoContent();
    // }
}