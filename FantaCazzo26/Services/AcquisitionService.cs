using FantaCazzo26.Data;
using FantaCazzo26.Enums;
using FantaCazzo26.Models;
using FantaCazzo26.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FantaCazzo26.Services
{
    public class AcquisitionService : IAcquisitionService
    {
        private readonly FantaCazzo26Context _context;


        public AcquisitionService(FantaCazzo26Context context)
        {
            _context = context;
        }



        public async Task<Acquisition> BuyPlayer(
            long playerId,
            long teamId,
            int acquisitionPrice)
        {

            Player player = await _context.Players
                .FirstOrDefaultAsync(p => p.Id == playerId)
                ?? throw new Exception(
                    $"Giocatore non trovato con ID: {playerId}"
                );


            Squad squad = await _context.Squads
                .FirstOrDefaultAsync(s => s.Id == teamId)
                ?? throw new Exception(
                    $"Squadra non trovata con ID: {teamId}"
                );



            switch (player.Role)
            {
                case "P":

                    PerformPlayerChecks(
                        player,
                        squad,
                        MaxPlayers.MaxPortieri,
                        acquisitionPrice
                    );

                    squad.NumGk++;
                    break;


                case "D":

                    PerformPlayerChecks(
                        player,
                        squad,
                        MaxPlayers.MaxDifensori,
                        acquisitionPrice
                    );

                    squad.NumDef++;
                    break;


                case "C":

                    PerformPlayerChecks(
                        player,
                        squad,
                        MaxPlayers.MaxCentrocampisti,
                        acquisitionPrice
                    );

                    squad.NumMid++;
                    break;


                case "A":

                    PerformPlayerChecks(
                        player,
                        squad,
                        MaxPlayers.MaxAttaccanti,
                        acquisitionPrice
                    );

                    squad.NumStr++;
                    break;


                default:
                    throw new ArgumentException(
                        "Ruolo giocatore non valido"
                    );
            }



            squad.Credits -= acquisitionPrice;

            player.Sold = true;



            Acquisition acquisition = new()
            {
                Player = player,
                Squad = squad,
                AcquisitionPrice = acquisitionPrice
            };


            await _context.Acquisitions.AddAsync(acquisition);

            await _context.SaveChangesAsync();


            return acquisition;
        }





        private void PerformPlayerChecks(
            Player player,
            Squad squad,
            int maxPlayers,
            int acquisitionPrice)
        {

            if (player.Sold)
                throw new Exception("Giocatore già venduto");



            switch(player.Role)
            {
                case "P":

                    if (squad.NumGk >= maxPlayers)
                        throw new Exception(
                            $"Non puoi avere più di {maxPlayers} portieri"
                        );

                    break;


                case "D":

                    if (squad.NumDef >= maxPlayers)
                        throw new Exception(
                            $"Non puoi avere più di {maxPlayers} difensori"
                        );

                    break;


                case "C":

                    if (squad.NumMid >= maxPlayers)
                        throw new Exception(
                            $"Non puoi avere più di {maxPlayers} centrocampisti"
                        );

                    break;


                case "A":

                    if (squad.NumStr >= maxPlayers)
                        throw new Exception(
                            $"Non puoi avere più di {maxPlayers} attaccanti"
                        );

                    break;
            }



            if (squad.Credits - acquisitionPrice < 0)
            {
                throw new Exception(
                    "Non bastano i crediti per questo acquisto"
                );
            }
        }





        public async Task SellPlayer(long playerId)
        {

            Acquisition? acquisition = await _context.Acquisitions
                .Include(a => a.Player)
                .Include(a => a.Squad)
                .FirstOrDefaultAsync(a => a.Player.Id == playerId);



            if (acquisition == null)
                throw new Exception(
                    "Acquisto non trovato"
                );



            Player player = acquisition.Player;

            Squad squad = acquisition.Squad;



            player.Sold = false;



            switch(player.Role)
            {
                case "P":
                    squad.NumGk--;
                    break;

                case "D":
                    squad.NumDef--;
                    break;

                case "C":
                    squad.NumMid--;
                    break;

                case "A":
                    squad.NumStr--;
                    break;
            }



            squad.Credits += acquisition.AcquisitionPrice / 2;



            _context.Acquisitions.Remove(acquisition);

            await _context.SaveChangesAsync();
        }





        public async Task<List<TeamResponse>?> GetFullTeam(long teamId)
        {
            List<Acquisition> acquisitions =
                await _context.Acquisitions
                    .Include(a => a.Player)
                    .Where(a => a.Squad.Id == teamId)
                    .ToListAsync();


            if (!acquisitions.Any())
                return null;


            return acquisitions
                .Select(a => new TeamResponse(
                    a.Player,
                    a.AcquisitionPrice
                ))
                .ToList();
        }
    }
}