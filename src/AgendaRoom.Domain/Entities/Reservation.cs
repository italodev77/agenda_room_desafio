using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AgendaRoom.Domain.Enums;

namespace AgendaRoom.Domain.Entities;

[Table("TB_Reservations")]
public class Reservation
{
        [Key]
        public int ReservationId { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore] 
        public User User { get; set; }

        [Required]
        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; }

        [Required]
        public DateTime ReservationDate { get; set; }  

        [Required]
        public DateTime ExpiryDate { get; set; }  

        [Required]
        public ReservationStatus Status { get; set; } = ReservationStatus.Active;
}