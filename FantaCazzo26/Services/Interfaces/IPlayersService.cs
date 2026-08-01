using FantaCazzo26.Models;

namespace FantaCazzo26.Services.Interfaces
{
    public interface IPlayersService
    {
        Task<List<Player>> LoadPlayers(string percorso);

        Task<List<Player>> FindBySquad(string squad);

        Task<Player?> FindById(long playerId);

        Task<List<Player>> GetAllPlayers();

        Task DeleteAllPlayers();
    }
}