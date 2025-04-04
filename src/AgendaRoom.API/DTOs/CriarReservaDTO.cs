namespace AgendaRoom.DTOs;

public class CriarReservaDTO
{
    public int UsuarioId { get; set; }
    public int SalaId { get; set; }
    public DateTime DataReserva { get; set; }
    public DateTime DataVencimento { get; set; }
}