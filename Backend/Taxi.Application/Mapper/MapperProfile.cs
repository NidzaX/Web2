using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Application.Dto.Queries;
using Taxi.Domain.Rides;
using Taxi.Domain.Users;

namespace Taxi.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, GetUserDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username.Value))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName.Value))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName.Value))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password.Value))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address.Value))
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.UserType))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.Value))
                .ForMember(dest => dest.File, opt => opt.MapFrom(src => src.Picture != null ? src.Picture.Value : null));

            CreateMap<Ride, GetUserRideDto>()
                .ForMember(dest => dest.RideId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => src.DriverId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Value))
                .ForMember(dest => dest.PredictedTime, opt => opt.MapFrom(src => src.PredictedTime.Value))
                .ForMember(dest => dest.WaitingTime, opt => opt.MapFrom(src => src.WaitingTime.Value))
                .ForMember(dest => dest.StartAddress, opt => opt.MapFrom(src => src.StartAddress.Value))
                .ForMember(dest => dest.EndAddress, opt => opt.MapFrom(src => src.EndAddress.Value));

            CreateMap<Ride, GetAvailableRidesDto>()
                .ForMember(dest => dest.RideId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.PredictedTime, opt => opt.MapFrom(src => src.PredictedTime.Value))
                .ForMember(dest => dest.StartAddress, opt => opt.MapFrom(src => src.StartAddress.Value))
                .ForMember(dest => dest.EndAddress, opt => opt.MapFrom(src => src.EndAddress.Value))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Value));

            CreateMap<Ride, GetCompletedRidesDto>()
                .ForMember(dest => dest.RideId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.PredictedTime, opt => opt.MapFrom(src => src.PredictedTime.Value))
                .ForMember(dest => dest.StartAddress, opt => opt.MapFrom(src => src.StartAddress.Value))
                .ForMember(dest => dest.EndAddress, opt => opt.MapFrom(src => src.EndAddress.Value))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Value))
                .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => src.DriverId))
                .ForMember(dest => dest.WaitingTime, opt => opt.MapFrom(src => src.WaitingTime.Value));

            CreateMap<Ride, GetAllRidesDto>()
               .ForMember(dest => dest.RideId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
               .ForMember(dest => dest.PredictedTime, opt => opt.MapFrom(src => src.PredictedTime.Value))
               .ForMember(dest => dest.StartAddress, opt => opt.MapFrom(src => src.StartAddress.Value))
               .ForMember(dest => dest.EndAddress, opt => opt.MapFrom(src => src.EndAddress.Value))
               .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Value))
               .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => src.DriverId))
               .ForMember(dest => dest.WaitingTime, opt => opt.MapFrom(src => src.WaitingTime.Value));
        }

    }

}
