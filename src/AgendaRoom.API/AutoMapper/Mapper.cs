using AgendaRoom.API.DTOs;
using AgendaRoom.Domain.Entities;
using AutoMapper;

namespace AgendaRoom.API.AutoMapper;

public class Mapper: Profile
{
    public MappingProfile()
    {
        // Mapeamento de Reservation
        CreateMap<Reservation, CancelReservationDTO>().ReverseMap();
        CreateMap<Reservation, CreateReservationDTO>().ReverseMap();

        // Mapeamento de Salas
        CreateMap<Room, CreateRoomDTO>().ReverseMap();
        CreateMap<Room, UpdateSalaDTO>().ReverseMap();

        // Mapeamento de Usuários
        CreateMap<User, RegisterDTO>().ReverseMap();
        CreateMap<User, LoginDTO>().ReverseMap();
        CreateMap<User, UpdateUserDTO>().ReverseMap();
    }
}