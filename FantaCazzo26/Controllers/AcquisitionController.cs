using FantaCazzo26.Models;
using FantaCazzo26.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FantaCazzo26.Controllers
{
    [ApiController]
    [Route("acquisition")]
    public class AcquisitionController : ControllerBase
    {
        private readonly IAcquisitionService _acquisitionService;


        public AcquisitionController(
            IAcquisitionService acquisitionService)
        {
            _acquisitionService = acquisitionService;
        }



        [HttpPost("add-player-to-team")]
        public async Task<IActionResult> BuyPlayer(
            [FromQuery] long playerId,
            [FromQuery] long teamId,
            [FromQuery] int acquisitionPrice)
        {
            try
            {
                Acquisition acquisition =
                    await _acquisitionService.BuyPlayer(
                        playerId,
                        teamId,
                        acquisitionPrice
                    );


                return Ok(
                    $"Acquisto avvenuto con successo {acquisition}"
                );
            }
            catch(Exception e)
            {
                return BadRequest(
                    "Errore durante l'acquisto"
                );
            }
            
        }




        [HttpDelete("sell-player/{playerId}")]
        public async Task<IActionResult> SellPlayer(
            [FromRoute] long playerId)
        {
            try
            {
                await _acquisitionService.SellPlayer(playerId);

                return Ok(
                    "Giocatore svincolato con successo"
                );
            }
            catch(Exception)
            {
                return BadRequest(
                    "Errore durante lo svincolamento"
                );
            }
        }





        [HttpGet("get-full-team/{teamId}")]
        public async Task<ActionResult<List<TeamResponse>>> GetFullTeam(
            [FromRoute] long teamId)
        {

            List<TeamResponse>? team =
                await _acquisitionService.GetFullTeam(teamId);


            if(team != null)
            {
                return Ok(team);
            }


            return NotFound();
        }
    }
}