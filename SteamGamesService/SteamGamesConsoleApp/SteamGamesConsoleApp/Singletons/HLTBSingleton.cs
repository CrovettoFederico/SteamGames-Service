using SteamGamesConsoleApp.Models;
using SteamGamesConsoleApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;
using SteamGamesConsoleApp.Models.HowLongToBeatModels;
using System.Configuration;

namespace SteamGamesConsoleApp.Singletons {
    public static class HLTBSingleton {

        private static string SearchUrl = Context.Configuration["HLTBSearchUrl"];
        private static string DetailUrl = Context.Configuration["HLTBDetailUrl"];

        public static Duracion ConseguirCuantoDura(string NombreJuego) {
            try {
                HLTBResponse Juego = new HLTBResponse();

                using (var client = new HttpClient()) {
                    string JsonGameSearch = "";
                    var Response = client.GetAsync(SearchUrl + NombreJuego).Result;
                    JsonGameSearch = Response.Content.ReadAsStringAsync().Result;

                    List<HLTBResponse> JuegosEncontrados = JsonConvert.DeserializeObject<List<HLTBResponse>>(JsonGameSearch);
                    var JuegoId = JuegosEncontrados.MaxBy(x => x.similarity).id;
                    Response = client.GetAsync(DetailUrl + JuegoId.ToString()).Result;
                    JsonGameSearch = Response.Content.ReadAsStringAsync().Result;
                    Juego = JsonConvert.DeserializeObject<HLTBResponse>(JsonGameSearch);

                }

                return new Duracion() {
                    Horas = Juego.gameplayMainExtra,
                    Juego = NombreJuego,
                    _Duracion = GetDuracionEnum(Juego.gameplayMainExtra)
                };
            }catch (Exception e) {
                throw new Exception("Error al conseguir duracion del juego " + NombreJuego, e );
            }
        }

        private static DuracionEnum GetDuracionEnum(double horas) {
            if (horas > 0 && horas <= 10)
                return DuracionEnum.Cortos;
            if (horas > 10 && horas <= 20)
                return DuracionEnum.Medianos;
            if (horas > 20)
                return DuracionEnum.Largos;

            return DuracionEnum.NULL;
        }
    }
}
