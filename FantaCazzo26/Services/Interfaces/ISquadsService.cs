using FantaCazzo26.Models;

namespace FantaCazzo26.Services.Interfaces;

public interface ISquadsService
{
    Task<Squad> AddTeam(string name, string president, int credits);

    Task<List<Squad>> GetAll();

    Task DeleteTeam(long id);

    Task AddCredits(long id, int credits);

    Task<Squad?> FindById(long id);
}