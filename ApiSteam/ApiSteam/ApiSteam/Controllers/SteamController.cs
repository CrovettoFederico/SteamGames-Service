using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using ApiSteam.Utils;
using System.Xml;
using System.Xml.Serialization;
using ApiSteam.Models;
using System.Xml.Linq;

namespace ApiSteam.Controllers {

    [ApiController]
    [Route("Steam")]
    public class SteamController : ControllerBase{

        private string OwnedGamesPath = Configuration.OwnedGamesPath;

        public SteamController() {
            
            
        }

        [HttpGet("GetOwnedGames")]
        public IActionResult GetOwnedGames() {

            try {
                gamesList Games = Steam.GetGamesList(OwnedGamesPath);

                return new JsonResult(Games);

            }catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("GetHS")]
        public IActionResult GetHS() {

            try {
                string Games = HowLongToBeat.GetHowLongForGame("Rocket League");

                return new JsonResult(Games);

            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
