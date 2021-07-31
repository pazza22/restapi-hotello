using AutoMapper;
using HotelAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelAPI.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RoomEntity, Room>()
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate / 100.0m))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src => Link.To(nameof(
                     Controllers.RoomsController.GetRoomByID), new { roomId = src.Id })));

        }
    }
}
