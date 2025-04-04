using AgendaRoom.Config;
using AgendaRoom.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgendaRoom.DALs;

public class SalasDAL
{
    private readonly ApiDbContext _dbContext;

    public SalasDAL(ApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Salas>> GetAllSalas()
    {
        return await _dbContext.Salas.ToListAsync();
    }
    
    public async Task<Salas> GetSalasById(int id)
    {
        return await _dbContext.Salas.FindAsync(id);
    }

    public async Task<Salas> CreateSalas(Salas salas)
    {
        _dbContext.Salas.Add(salas);
        await _dbContext.SaveChangesAsync();
        return salas;
    }
    public async Task UpdateSala(Salas sala)
    {
        _dbContext.Salas.Update(sala);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteSala(int id)
    {
        var sala = await _dbContext.Salas.FindAsync(id);
        if (sala != null)
        {
            _dbContext.Salas.Remove(sala);
            await _dbContext.SaveChangesAsync();
        }
    } 
}