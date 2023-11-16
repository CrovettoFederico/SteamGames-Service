using ApiSteam.Models;
using Microsoft.AspNetCore.Mvc;
using ApiSteam.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ApiSteam.Controllers {
    [ApiController]
    [Route("Personas")]
    public class PersonaController : ControllerBase {

        const string RutaArchivoPersonas = "C:\\Users\\Friki\\Desktop\\ListaPersonas.fede";

        [ApiSteamAuthAttribute]
        [HttpPost("PostPersonas")]
        public ActionResult PostPersonas([FromBody]Persona PersonaFltro) {            
            var Lista = JsonReader.LeerPersonas(RutaArchivoPersonas);
            var ListaFiltrada = Lista.Where(x=> x.Edad >= PersonaFltro.Edad).ToList();
            return new JsonResult( ListaFiltrada);
        }
    }

}
