using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaRoom.Domain.Entities;
[Table("TB_Rooms")]
public class Room
{
    public int RoomId { get; set; }
    
    [Required]
    public string name { get; set; } = string.Empty;
    
    [Required]
    public int capacity { get; set; } 
}