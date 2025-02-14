using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Context;

namespace Restaurants.Infrastructure.Seeders;

internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
{
    // seed data into database initially if table has no table.
    public async Task SeedAsync()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                dbContext.Restaurants.AddRange(restaurants);
                await dbContext.SaveChangesAsync();
            }
        }
    }
    
    // Pre defined data for seed 
    private IEnumerable<Restaurant> GetRestaurants()
    {
        var restaurant = new Restaurant();
        restaurant.Name = "KFC";
        restaurant.Category = "Fast Food";
        restaurant.Description = "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.";
        restaurant.ContactEmail = "contact@kfc.com";
        restaurant.HasDelivery = true;
        restaurant.Dishes = [
            new ()
            {
                Name = "Nashville Hot Chicken",
                Description = "Nashville Hot Chicken (10 pcs.)",
                Price = 10.30M,
            },

            new ()
            {
                Name = "Chicken Nuggets",
                Description = "Chicken Nuggets (5 pcs.)",
                Price = 5.30M,
            },
        ];
        restaurant.Address = new ()
        {
            City = "London",
            Street = "Cork St 5",
            ZipCode = "WC2N 5DU",
            State = "FL",
            Country = "USA"
        };
        List<Restaurant> restaurants = [
            restaurant,
            new ()
            {
                Name = "McDonald",
                Category = "Fast Food",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                ContactEmail = "contact@mcdonald.com",
                HasDelivery = true,
                Address = new Address()
                {
                    City = "London",
                    Street = "Boots 193",
                    ZipCode = "W1F 8SR",
                    State = "TX",
                    Country = "USA"
                }
            }
        ];
        return restaurants;
    }
}