using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Authorization;

public class RestaurantsUserClaimsPrincipalFactory(UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager,
    IOptions<IdentityOptions> optionsAccessor) 
    : UserClaimsPrincipalFactory<User, IdentityRole>(userManager, roleManager, optionsAccessor)
{
    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var id = await GenerateClaimsAsync(user);
        
        if(user.Nationality != null)
            id.AddClaim(new Claim("nationality", user.Nationality));
        
        if(user.DateOfBirth != null)
            id.AddClaim(new Claim("dateOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-dd")));

        return new ClaimsPrincipal(id);
    }
}