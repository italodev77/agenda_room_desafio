using AgendaRoom.Config;
using AgendaRoom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgendaRoom.DALs;

public class RoomDal
{
    private readonly ApiDbContext _dbContext;

    public RoomDal(ApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Room>> GetAllRooms()
    {
        return await _dbContext.Rooms.ToListAsync();
    }
    
    public async Task<Room> GetRoomById(int id)
    {
        return await _dbContext.Rooms.FindAsync(id);
    }

    public async Task<Room> CreateRooms(Room rooms)
    {
        _dbContext.Rooms.Add(rooms);
        await _dbContext.SaveChangesAsync();
        return rooms;
    }
    public async Task UpdateRoom(Room room)
    {
        _dbContext.Rooms.Update(room);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteRoom(int id)
    {
        var room = await _dbContext.Rooms.FindAsync(id);
        if (room != null)
        {
            _dbContext.Rooms.Remove(room);
            await _dbContext.SaveChangesAsync();
        }
    } 
}