using MediatR;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommand : IRequest<int>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Category { get; set; } = null!;
    public bool HasDelivery { get; set; }
    public string? ContactNumber { get; set; }
    public string? ContactEmail { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; } 
    public string? State { get; set; } 
    public string? ZipCode { get; set; } 
    public string? Country { get; set; }
}