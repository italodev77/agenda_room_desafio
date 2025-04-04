using AgendaRoom.Config;
using AgendaRoom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgendaRoom.Infrastructure.DALs;

public class UserDAL
{
    private readonly ApiDbContext _dbContext;

    public UserDAL(ApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

        
    public async Task<List<User>> GetAllUsers()
    {
        return await _dbContext.User.ToListAsync();
    }
    public async Task<User> GetUserById(int id)
    {
        return await _dbContext.User.FindAsync(id);
    }
        
    public async Task<User> GetUserByEmail(string email)
    {
        return await _dbContext.User.FirstOrDefaultAsync(u => u.email == email);
    }
        
    public async Task UpdateUser(User user)
    {
        _dbContext.User.Update(user);
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteUser(int id)
    {
        var user = await _dbContext.User.FindAsync(id);
        if (user != null)
        {
            _dbContext.User.Remove(user);
            await _dbContext.SaveChangesAsync();
        }
    } 
}