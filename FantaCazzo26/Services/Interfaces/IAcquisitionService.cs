using FantaCazzo26.Models;

namespace FantaCazzo26.Services.Interfaces
{
    public interface IAcquisitionService
    {
        Task<Acquisition> BuyPlayer(long playerId, long teamId, int acquisitionPrice);

        Task SellPlayer(long playerId);

        Task<List<TeamResponse>?> GetFullTeam(long teamId);
    }
}