using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Tests.Application;

public class UpdateRestaurantCommandHandlerTests
{
    private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _loggerMock;
    private readonly Mock<IRestaurantsRepo> _restaurantsRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IRestaurantAuthorizationService> _restaurantAuthorizationServiceMock;

    private readonly UpdateRestaurantCommandHandler _handler;

    public UpdateRestaurantCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
        _restaurantsRepositoryMock = new Mock<IRestaurantsRepo>();
        _mapperMock = new Mock<IMapper>();
        _restaurantAuthorizationServiceMock = new Mock<IRestaurantAuthorizationService>();

        _handler = new UpdateRestaurantCommandHandler(
            _loggerMock.Object,
            _restaurantsRepositoryMock.Object,
            _mapperMock.Object,
            _restaurantAuthorizationServiceMock.Object);
    }

    [Fact()]
    public async Task Handle_WithValidRequest_ShouldUpdateRestaurants()
    {
        // arrange
        var restaurantId = 1;
        var command = new UpdateRestaurantCommand()
        {
            Id = restaurantId,
            Name = "New Test",
            Description = "New Description",
            HasDelivery = true,
        };

        var restaurant = new Restaurant()
        {
            Id = restaurantId,
            Name = "Test",
            Description = "Test",
        };

        _restaurantsRepositoryMock.Setup(r => r.GetRestaurantByIdAsync(restaurantId))
            .ReturnsAsync(restaurant);

        _restaurantAuthorizationServiceMock.Setup(m => m.Authorize(restaurant, Domain.Constants.ResourceOperation.Update))
            .Returns(true);


        // act
        await _handler.Handle(command, CancellationToken.None);

        // assert
         
        _restaurantsRepositoryMock.Verify(r => r.UpdateRestaurant(), Times.Once);
        _mapperMock.Verify(m => m.Map(command, restaurant), Times.Once);
    }

    // [Fact]
    // public async Task Handle_WithNonExistingRestaurant_ShouldThrowNotFoundException()
    // {
    //     // Arrange
    //     var restaurantId = 3;
    //     var request = new UpdateRestaurantCommand
    //     {
    //         Id = restaurantId
    //     };
    //
    //     _restaurantsRepositoryMock.Setup(r => r.GetRestaurantByIdAsync(restaurantId))
    //             .ReturnsAsync((Restaurant?)null);
    //
    //     // act
    //
    //     Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);
    //
    //     // assert
    //     await act.Should().ThrowAsync<NotFoundException>()
    //             .WithMessage($"Restaurant with id: {restaurantId} doesn't exist");
    // }

    [Fact]
    public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidException()
    {
        // / Arrange
        var restaurantId = 3;
        var request = new UpdateRestaurantCommand
        {
            Id = restaurantId
        };

        var existingRestaurant = new Restaurant
        {
            Id = restaurantId
        };

        _restaurantsRepositoryMock
            .Setup(r => r.GetRestaurantByIdAsync(restaurantId))
                .ReturnsAsync(existingRestaurant);

        _restaurantAuthorizationServiceMock
            .Setup(a => a.Authorize(existingRestaurant, ResourceOperation.Update))
                .Returns(false);

        // act

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ForbidException>();
    }
}