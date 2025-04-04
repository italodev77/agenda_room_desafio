using AgendaRoom.Config;
using AgendaRoom.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgendaRoom.Enums;

namespace AgendaRoom.DALs
{
    public class ReservaDAL
    {
        private readonly ApiDbContext _dbContext;

        public ReservaDAL(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Reservas>> GetAllReservas()
        {
            return await _dbContext.Reservas.ToListAsync();
        }

        public async Task<List<Reservas>> BuscarReservas(int? usuarioId, int? salaId, DateTime? dataReserva, StatusReserva? status)
        {
            var query = _dbContext.Reservas.AsQueryable();

            
            if (usuarioId.HasValue)
                query = query.Where(r => r.UsuarioId == usuarioId.Value);

            
            if (salaId.HasValue)
                query = query.Where(r => r.SalaId == salaId.Value);

            
            if (dataReserva.HasValue)
                query = query.Where(r => r.DataReserva.Date == dataReserva.Value.Date);

            
            if (status.HasValue)
                query = query.Where(r => r.Status == status.Value);

            return await query
                .Include(r => r.Usuario)
                .Include(r => r.Sala)
                .ToListAsync();
        }



        public async Task<bool> CancelarReserva(int reservaId)
        {
            var reserva = await _dbContext.Reservas.FindAsync(reservaId);
            if (reserva == null)
                return false;

            if (reserva.Status == StatusReserva.Cancelada)
                return false; 

            reserva.Status = StatusReserva.Cancelada;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExisteConflitoDeHorario(int salaId, DateTime dataReserva, DateTime dataVencimento)
        {
            return await _dbContext.Reservas.AnyAsync(r =>
                r.SalaId == salaId &&
                r.Status == StatusReserva.Ativa && 
                (
                    (dataReserva >= r.DataReserva && dataReserva < r.DataVencimento) || 
                    (dataVencimento > r.DataReserva && dataVencimento <= r.DataVencimento) ||
                    (dataReserva <= r.DataReserva && dataVencimento >= r.DataVencimento) 
                )
            );
        }

        public async Task<string> CriarReserva(Reservas reserva)
        {
            if (reserva.DataReserva.Date != reserva.DataVencimento.Date)
                return "Erro: A reserva deve iniciar e terminar no mesmo dia.";

         
            bool conflito = await ExisteConflitoDeHorario(reserva.SalaId, reserva.DataReserva, reserva.DataVencimento);
            if (conflito)
                return "Erro: Já existe uma reserva para essa sala nesse horário.";

            _dbContext.Reservas.Add(reserva);
            await _dbContext.SaveChangesAsync();
            return "Reserva criada com sucesso!";
        }
    }
}