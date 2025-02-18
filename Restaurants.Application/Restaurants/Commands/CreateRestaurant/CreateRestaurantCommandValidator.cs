using FluentValidation;
using Restaurants.Application.Dtos;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> _validCategories = ["Italian", "Mexican", "French", "Indian", "Japanese", "Korean", "American"];
    public CreateRestaurantCommandValidator()
    {
        RuleFor(dto => dto.Name).Length(3, 100);
        RuleFor(dto => dto.Category).Must(_validCategories.Contains)
            .WithMessage("Invalid category, Please choose a valid category.");
        // RuleFor(dto => dto.Description).NotEmpty().WithMessage("Description is required");
        // RuleFor(dto => dto.Category).NotEmpty().WithMessage("Insert a valid category");
        RuleFor(dto => dto.ContactEmail).NotEmpty().EmailAddress().WithMessage("Enter a valid email");
        RuleFor(dto => dto.ContactNumber).NotEmpty().WithMessage("Enter a valid contact number");
        RuleFor(dto => dto.ZipCode).Matches(@"^\d{5}-\d{2}$").WithMessage("Enter a valid zip code (xxxxx - xx)");
    }
}