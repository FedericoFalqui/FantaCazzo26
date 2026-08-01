using FantaCazzo26.Data;
using FantaCazzo26.Models;
using FantaCazzo26.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FantaCazzo26.Services;

public class SquadsService : ISquadsService
{
    private readonly FantaCazzo26Context _context;

    public SquadsService(FantaCazzo26Context context)
    {
        _context = context;
    }

    public async Task<Squad> AddTeam(string name, string president, int credits)
    {
        var squad = new Squad
        {
            Name = name,
            President = president,
            Credits = credits,
            NumDef = 0,
            NumGk = 0,
            NumMid = 0,
            NumStr = 0
        };

        _context.Squads.Add(squad);

        await _context.SaveChangesAsync();

        return squad;
    }

    public async Task<List<Squad>> GetAll()
    {
        return await _context.Squads.ToListAsync();
    }

    public async Task<Squad?> FindById(long id)
    {
        return await _context.Squads.FindAsync(id);
    }

    public async Task DeleteTeam(long id)
    {
        var squad = await _context.Squads.FindAsync(id);

        if (squad == null)
            return;

        _context.Squads.Remove(squad);

        await _context.SaveChangesAsync();
    }

    public async Task AddCredits(long id, int credits)
    {
        var squad = await _context.Squads.FindAsync(id);

        if (squad == null)
            return;

        squad.Credits += credits;

        await _context.SaveChangesAsync();
    }
}