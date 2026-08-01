using FantaCazzo26.Models;

namespace FantaCazzo26.Services.Interfaces
{
    public interface IPlayersRepository
    {
        Task<Player> Save(Player player);

        Task<List<Player>> FindAll();

        Task<Player?> FindById(long id);

        Task<List<Player>> FindPlayersBySquad(string squad);

        Task DeleteAll();
    }
}