namespace AgendaRoom.API.DTOs;

public class CreateReservationDTO
{
    public int UserId { get; set; }
    public int RoomId { get; set; }
    public DateTime DataReserva { get; set; }
    public DateTime DataVencimento { get; set; }
}