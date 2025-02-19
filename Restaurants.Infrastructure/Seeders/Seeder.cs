using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Context;

namespace Restaurants.Infrastructure.Seeders;

internal class Seeder(RestaurantsDbContext dbContext) : ISeeder
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

            if (!dbContext.Roles.Any())
            {
                var roles = GetIdentityRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private static IEnumerable<IdentityRole> GetIdentityRoles()
    {
        var roles = new List<IdentityRole>
        {
            new (UserRoles.User)
            {
                NormalizedName = UserRoles.User.ToUpper()
            },
            new (UserRoles.Admin)
            {
                NormalizedName = UserRoles.Admin.ToUpper()
            },
            new (UserRoles.Owner)
            {
                NormalizedName = UserRoles.Owner.ToUpper()
            }
        };
        return roles;
    }
    
    // Pre defined data for seed 
    private static IEnumerable<Restaurant> GetRestaurants()
    {
        var restaurant = new Restaurant
        {
            Name = "KFC",
            Category = "Fast Food",
            Description = "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
            ContactEmail = "contact@kfc.com",
            HasDelivery = true,
            Dishes =
            [
                new Dish
                {
                    Name = "Nashville Hot Chicken",
                    Description = "Nashville Hot Chicken (10 pcs.)",
                    Price = 10.30M,
                },

                new Dish
                {
                    Name = "Chicken Nuggets",
                    Description = "Chicken Nuggets (5 pcs.)",
                    Price = 5.30M,
                },
            ],
            Address = new Address
            {
                City = "London",
                Street = "Cork St 5",
                ZipCode = "WC2N 5DU",
                State = "FL",
                Country = "USA"
            }
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
                Address = new Address
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