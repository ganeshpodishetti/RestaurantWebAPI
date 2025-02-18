using FluentValidation;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandlerValidator : AbstractValidator<CreateDishCommand>
{
    public CreateDishCommandHandlerValidator()
    {
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Price must be a non-negative number");
        RuleFor(x => x.KiloCalories).GreaterThanOrEqualTo(0).WithMessage("KiloCalories must be a non-negative number");
    }
}