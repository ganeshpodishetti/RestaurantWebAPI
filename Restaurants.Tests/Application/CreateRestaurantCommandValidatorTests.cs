using FluentValidation.TestHelper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;

namespace Restaurants.Tests.Application;

public class CreateRestaurantCommandValidatorTests
{
    // [Fact()]
    // public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    // {
    //     // arrange
    //
    //     var command = new CreateRestaurantCommand()
    //     {
    //         Name = "Test",
    //         Category = "Indian",
    //         ContactEmail = "test@test.com",
    //         ZipCode = "12123-45",
    //     };
    //
    //     var validator = new CreateRestaurantCommandValidator();
    //
    //     // act
    //
    //     var result = validator.TestValidate(command);
    //
    //     // assert
    //
    //     result.ShouldNotHaveAnyValidationErrors();
    // }

    // [Fact()]
    // public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
    // {
    //     // arrange
    //
    //     var command = new CreateRestaurantCommand()
    //     {
    //         Name = "Te",
    //         Category = "Ita",
    //         ContactEmail = "@test.com",
    //         ZipCode = "12345-23",
    //     };
    //
    //     var validator = new CreateRestaurantCommandValidator();
    //
    //     // act
    //
    //     var result = validator.TestValidate(command);
    //
    //     // assert
    //
    //     result.ShouldHaveValidationErrorFor(c => c.Name);
    //     result.ShouldHaveValidationErrorFor(c => c.Category);
    //     result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
    //     result.ShouldHaveValidationErrorFor(c => c.ZipCode);
    // }


    [Theory()]
    [InlineData("Italian")]
    [InlineData("Mexican")]
    [InlineData("Japanese")]
    [InlineData("American")]
    [InlineData("Indian")]
    public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
    {
        // arrange
        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand { Category = category };

        // act

        var result = validator.TestValidate(command);

        // assert
        result.ShouldNotHaveValidationErrorFor(c => c.Category);

    }

    [Theory()]
    [InlineData("10220")]
    [InlineData("102-20")]
    [InlineData("10 220")]
    [InlineData("10-2 20")]
    public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForPostalCodeProperty(string zipcode)
    {
        // arrange
        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand { ZipCode = zipcode };

        // act

        var result = validator.TestValidate(command);

        // assert
        result.ShouldHaveValidationErrorFor(c => c.ZipCode);
    }
}