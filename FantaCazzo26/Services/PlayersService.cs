using FantaCazzo26.Data;
using FantaCazzo26.Models;
using Microsoft.EntityFrameworkCore;

namespace FantaCazzo26.Services
{
    public class PlayersService
    {
        private readonly FantaCazzo26Context _context;

        public PlayersService(FantaCazzo26Context context)
        {
            _context = context;
        }


        public async Task<List<Player>> LoadPlayers(string percorso)
        {
            List<Player> lista = new();


            using StreamReader file = new StreamReader(percorso);

            // Salta intestazione CSV
            await file.ReadLineAsync();


            while (!file.EndOfStream)
            {
                string? linea = await file.ReadLineAsync();

                if (string.IsNullOrWhiteSpace(linea))
                    continue;


                string[] riga = linea.Split(';');


                Player player = new Player
                {
                    Role = riga[0],
                    Name = riga[1],
                    Squad = riga[2],
                    SuggestedPrice = int.Parse(riga[3]),
                    Sold = false
                };


                await _context.Players.AddAsync(player);

                lista.Add(player);
            }


            await _context.SaveChangesAsync();

            return lista;
        }



        public async Task<List<Player>> FindBySquad(string squad)
        {
            return await _context.Players
                .Where(p => p.Squad == squad)
                .ToListAsync();
        }



        public async Task<Player?> FindById(long playerId)
        {
            return await _context.Players
                .FirstOrDefaultAsync(p => p.Id == playerId);
        }



        public async Task<List<Player>> GetAllPlayers()
        {
            return await _context.Players
                .ToListAsync();
        }



        public async Task DeleteAllPlayers()
        {
            var players = await _context.Players.ToListAsync();

            _context.Players.RemoveRange(players);

            await _context.SaveChangesAsync();
        }
    }
}