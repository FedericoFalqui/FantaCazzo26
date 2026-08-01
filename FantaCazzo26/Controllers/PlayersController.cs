using FantaCazzo26.Models;
using Microsoft.AspNetCore.Mvc;
using FantaCazzo26.Services;

namespace FantaCazzo26.Controllers{
    [ApiController]
    [Route("players")]
    public class PlayersController : ControllerBase
    {
        private readonly PlayersService _playersService;

        public PlayersController(PlayersService playersService)
        {
            _playersService = playersService;
        }


        [HttpPost("load-players")]
        public async Task<IActionResult> LoadPlayers([FromQuery(Name = "percorso")] string percorso)
        {
            try
            {
                List<Player> players = await _playersService.LoadPlayers(percorso);

                if (players.Count > 0)
                {
                    return Ok(new 
                    { 
                        message = "Giocatori caricati con successo" 
                    });
                }
                else
                {
                    return Ok(new 
                    { 
                        message = "Non è stato letto niente" 
                    });
                }
            }
            catch (FileNotFoundException)
            {
                return NotFound(new
                {
                    message = "File non trovato"
                });
            }
        }


        [HttpGet("find-all")]
        public async Task<ActionResult<List<Player>>> GetAllPlayers()
        {
            List<Player> players = await _playersService.GetAllPlayers();

            return Ok(players);
        }


        [HttpGet("find-by-squad")]
        public async Task<IActionResult> FindPlayersBySquad(
            [FromQuery(Name = "squad")] string squad)
        {
            List<Player> players = await _playersService.FindBySquad(squad);

            if (players.Count > 0)
            {
                return Ok(players);
            }

            return Ok(new
            {
                message = $"Non ci sono giocatori che appartengono alla squadra: {squad}"
            });
        }


        [HttpDelete("delete-all")]
        public async Task<IActionResult> DeleteAllPlayers()
        {
            await _playersService.DeleteAllPlayers();

            return Ok(new
            {
                message = "Tutti i giocatori sono stati cancellati"
            });
        }
    }
}