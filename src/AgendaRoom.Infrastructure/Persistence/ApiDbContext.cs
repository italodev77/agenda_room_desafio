using AgendaRoom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgendaRoom.Config;
public class ApiDbContext : DbContext
{
        public ApiDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
}
