using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dtos;

public class DishDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }

    public int? KiloCalories { get; set; }
}