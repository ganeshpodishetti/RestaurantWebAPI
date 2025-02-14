namespace Restaurants.Domain.Entities;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Category { get; set; } = null!;
    public bool HasDelivery { get; set; }
    
    public string? ContactNumber { get; set; }
    public string? ContactEmail { get; set; }
    
    public Address? Address { get; set; }
    public List<Dish> Dishes { get; set; } = new List<Dish>();
}