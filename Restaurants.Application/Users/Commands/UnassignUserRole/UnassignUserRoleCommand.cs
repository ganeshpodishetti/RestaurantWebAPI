using MediatR;

namespace Restaurants.Application.Users.Commands.UnassignUserRole;

public class UnassignUserRoleCommand : IRequest
{
    public string UserEmail { get; set; } = null!;
    public string RoleName { get; set; } = null!;
}