using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AgendaRoom.API.DTOs;
using AgendaRoom.Domain.Entities;
using AgendaRoom.Domain.Enums;
using AgendaRoom.Infrastructure.DALs;

namespace AgendaRoom.API.Controllers
{
    [Route("api/reservation")]
    [ApiController]
    [Authorize] 
    public class ReservationController : ControllerBase
    {
        private readonly ReservationDAL _reservationDal;

        public ReservationController(ReservationDAL reservationDal)
        {
            _reservationDal = reservationDal;
        }
        
        [HttpGet("list-reservations")]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _reservationDal.GetAllReservations();
            return Ok(reservations);
        }
        
        [HttpGet("search-params-reservations")]
        public async Task<IActionResult> GetReservationsByParams([FromQuery] int? UserId, [FromQuery] int? RoomId, [FromQuery] DateTime? ReservationDate, [FromQuery] ReservationStatus? status)
        {
            var reservations = await _reservationDal.GetReservationsByParams(UserId, RoomId, ReservationDate, status);
            return Ok(reservations);
        }
        
        [HttpPost("create-reservation")]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDTO model)
        {
            
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não autenticado ou sem o claim 'idusuario'.");
            }
            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest("Id do usuário inválido no token.");
            }
            
            var reservation = new Reservation
            {
                UserId = userId,
                RoomId = model.RoomId,
                ReservationDate = model.ReservationDate,
                ExpiryDate = model.ExpiryDate,
                Status = ReservationStatus.Active
            };

            var result = await _reservationDal.CreateReservation(reservation);

            if (result.StartsWith("Erro"))
                return BadRequest(result);

            return Ok(result);
        }
        
        [HttpPut("cancel-reservation")]
        public async Task<IActionResult> CancelReservation([FromBody] CancelReservationDTO model)
        {
            bool success = await _reservationDal.CancelReservation(model.ReservationId);

            if (!success)
                return BadRequest("Erro: Reserva não encontrada ou já cancelada.");

            return Ok("Reserva cancelada com sucesso!");
        }
    }
}
