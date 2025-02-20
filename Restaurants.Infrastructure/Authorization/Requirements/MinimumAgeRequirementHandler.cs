using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastructure.Authorization.Requirements;

internal class MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger,
    IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();
        
        logger.LogInformation("User: {Email}, date of birth {DOB} - Handling MinimumAgeRequirement",
            currentUser.Email, currentUser.DateOfBirth);

        if (currentUser.DateOfBirth == null)
        {
            logger.LogInformation("user date of birth not set");
            context.Fail();
            return Task.CompletedTask;
        }

        if (currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
        {
            logger.LogInformation("authorization success");
            context.Succeed(requirement);
        }
        else
        {
            logger.LogInformation("authorization failed");
            context.Fail();
        }
        
        return Task.CompletedTask;
    }
}