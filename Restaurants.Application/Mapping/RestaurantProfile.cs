using AutoMapper;
using Restaurants.Application.Dtos;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Mapping;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<CreateRestaurantCommand, Restaurant>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(
                src => new Address
                {
                    City = src.City,
                    Country = src.Country,
                    ZipCode = src.ZipCode,
                    Street = src.Street,
                    State = src.State,
                }));
        
        
        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(dest => dest.City, opt =>
                opt.MapFrom(src => src.Address == null ? null : src.Address.City))
            .ForMember(dest => dest.Street, opt =>
                opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
            .ForMember(dest => dest.State, opt =>
                opt.MapFrom(src => src.Address == null ? null : src.Address.State))
            .ForMember(dest => dest.Country, opt =>
                opt.MapFrom(src => src.Address == null ? null : src.Address.Country))
            .ForMember(dest => dest.ZipCode, opt =>
                opt.MapFrom(src => src.Address == null ? null : src.Address.ZipCode));
    }
}