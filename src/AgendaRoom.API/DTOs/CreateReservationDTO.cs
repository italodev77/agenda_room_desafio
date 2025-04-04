namespace AgendaRoom.API.DTOs;

public class CreateReservationDTO
{
    public int UserId { get; set; }
    public int RoomId { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime ExpiryDate { get; set; }
}