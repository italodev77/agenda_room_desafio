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
        private readonly ReservaDAL _reservaDAL;

        public ReservasController(ReservaDAL reservaDAL)
        {
            _reservaDAL = reservaDAL;
        }
        
        [HttpGet("listar")]
        public async Task<IActionResult> GetAllReservas()
        {
            var reservas = await _reservaDAL.GetAllReservas();
            return Ok(reservas);
        }
        
        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarReservas([FromQuery] int? usuarioId, [FromQuery] int? salaId, [FromQuery] DateTime? dataReserva, [FromQuery] StatusReserva? status)
        {
            var reservas = await _reservaDAL.BuscarReservas(usuarioId, salaId, dataReserva, status);
            return Ok(reservas);
        }

        
        [HttpPost("criar")]
        public async Task<IActionResult> CriarReserva([FromBody] CriarReservaDTO model)
        {
            var reserva = new Reservas
            {
                UsuarioId = model.UsuarioId,
                SalaId = model.SalaId,
                DataReserva = model.DataReserva,
                DataVencimento = model.DataVencimento,
                Status = StatusReserva.Ativa
            };

            var resultado = await _reservaDAL.CriarReserva(reserva);

            if (resultado.StartsWith("Erro"))
                return BadRequest(resultado);

            return Ok(resultado);
        }
        
        [HttpPut("cancelar")]
        public async Task<IActionResult> CancelarReserva([FromBody] CancelarReservaDTO model)
        {
            bool sucesso = await _reservaDAL.CancelarReserva(model.ReservaId);

            if (!sucesso)
                return BadRequest("Erro: Reserva não encontrada ou já cancelada.");

            return Ok("Reserva cancelada com sucesso!");
        }
    }
}
