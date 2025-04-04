using AgendaRoom.Config;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgendaRoom.Domain.Entities;
using AgendaRoom.Domain.Enums;


namespace AgendaRoom.Infrastructure.DALs
{
    public class ReservationDAL
    {
        private readonly ApiDbContext _dbContext;

        public ReservationDAL(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Reservation>> GetAllReservations()
        {
            return await _dbContext.Reservations.ToListAsync();
        }

        public async Task<List<Reservation>> GetReservationsByParams(int? UserId, int? RoomId, DateTime? ReservationDate, ReservationStatus? status)
        {
            var query = _dbContext.Reservations.AsQueryable();

            
            if (UserId.HasValue)
                query = query.Where(r => r.UserId == UserId.Value);

            
            if (RoomId.HasValue)
                query = query.Where(r => r.RoomId == RoomId.Value);

            
            if (ReservationDate.HasValue)
                query = query.Where(r => r.ReservationDate.Date == ReservationDate.Value.Date);

            
            if (status.HasValue)
                query = query.Where(r => r.Status == status.Value);

            return await query
                .Include(r => r.User)
                .Include(r => r.Room)
                .ToListAsync();
        }



        public async Task<bool> CancelReservation(int ReservationId)
        {
            var reservation = await _dbContext.Reservations.FindAsync(ReservationId);
            if (reservation == null)
                return false;

            if (reservation.Status == ReservationStatus.Inactive)
                return false; 

            reservation.Status = ReservationStatus.Inactive;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ThereIsATimeConflict(int RoomId, DateTime ReservationDate, DateTime ExpiryDate)
        {
            return await _dbContext.Reservations.AnyAsync(r =>
                r.RoomId == RoomId &&
                r.Status == ReservationStatus.Active && 
                (
                    (ReservationDate >= r.ReservationDate && ReservationDate < r.ExpiryDate) || 
                    (ExpiryDate > r.ReservationDate && ExpiryDate<= r.ExpiryDate) ||
                    (ReservationDate <= r.ReservationDate && ExpiryDate >= r.ExpiryDate) 
                )
            );
        }

        public async Task<string> CreateReservation(Reservation reservation)
        {
            if (reservation.ReservationDate.Date != reservation.ExpiryDate.Date)
                return "Erro: A reserva deve iniciar e terminar no mesmo dia.";

         
            bool conflict = await ThereIsATimeConflict(reservation.RoomId, reservation.ReservationDate, reservation.ExpiryDate);
            if (conflict)
                return "Erro: Já existe uma reserva para essa sala nesse horário.";

            _dbContext.Reservations.Add(reservation);
            await _dbContext.SaveChangesAsync();
            return "Reserva criada com sucesso!";
        }
    }
}