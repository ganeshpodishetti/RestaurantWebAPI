using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dtos;

public class RestaurantDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Category { get; set; } = null!;
    public bool HasDelivery { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; } 
    public string? State { get; set; } 
    public string? ZipCode { get; set; } 
    public string? Country { get; set; }
    public List<DishDto> Dishes { get; set; } = [];
}