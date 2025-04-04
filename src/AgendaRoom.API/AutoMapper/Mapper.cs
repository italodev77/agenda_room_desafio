using AgendaRoom.API.DTOs;
using AgendaRoom.Domain.Entities;
using AutoMapper;

namespace AgendaRoom.API.AutoMapper;

public class Mapper: Profile
{
    public Mapper()
    {
       
        CreateMap<Reservation, CancelReservationDTO>().ReverseMap();
        CreateMap<Reservation, CreateReservationDTO>().ReverseMap();
        CreateMap<Room, CreateRoomDTO>().ReverseMap();
        CreateMap<Room, UpdateRoomDTO>().ReverseMap();
        CreateMap<User, RegisterDTO>().ReverseMap();
        CreateMap<User, LoginDTO>().ReverseMap();
        CreateMap<User, UpdateUserDTO>().ReverseMap();
    }
}