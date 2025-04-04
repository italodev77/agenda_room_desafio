using AgendaRoom.DALs;
using AgendaRoom.DTOs;
using AgendaRoom.Entities;
using AgendaRoom.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaRoom.Controllers
{
    [Route("api/reservas")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly ReservationDAL _reservationDal;

        public ReservasController(ReservationDAL reservationDal)
        {
            _reservationDal = reservationDal;
        }
        
        [HttpGet("listar")]
        public async Task<IActionResult> GetAllReservas()
        {
            var reservas = await _reservationDal.GetAllReservas();
            return Ok(reservas);
        }
        
        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarReservas([FromQuery] int? usuarioId, [FromQuery] int? salaId, [FromQuery] DateTime? dataReserva, [FromQuery] StatusReserva? status)
        {
            var reservas = await _reservationDal.BuscarReservas(usuarioId, salaId, dataReserva, status);
            return Ok(reservas);
        }

        
        [HttpPost("criar")]
        public async Task<IActionResult> CriarReserva([FromBody] CreateReservationDTO model)
        {
            var reserva = new Reservas
            {
                UsuarioId = model.UsuarioId,
                SalaId = model.SalaId,
                DataReserva = model.DataReserva,
                DataVencimento = model.DataVencimento,
                Status = StatusReserva.Ativa
            };

            var resultado = await _reservationDal.CriarReserva(reserva);

            if (resultado.StartsWith("Erro"))
                return BadRequest(resultado);

            return Ok(resultado);
        }
        
        [HttpPut("cancelar")]
        public async Task<IActionResult> CancelarReserva([FromBody] CancelReservation model)
        {
            bool sucesso = await _reservationDal.CancelarReserva(model.ReservaId);

            if (!sucesso)
                return BadRequest("Erro: Reserva não encontrada ou já cancelada.");

            return Ok("Reserva cancelada com sucesso!");
        }
    }
}
