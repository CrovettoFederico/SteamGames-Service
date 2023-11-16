using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;
using System.Web;
using static System.Net.WebRequestMethods;

namespace ApiSteam.Utils {
    public static class HowLongToBeat {

        const string BasePath = "https://howlongtobeat.com/";
        const string GamePath = "game/";
        const string IdSearcher = "title=\"[NOMBRE]\" href=\"/game/";

        public static string GetHowLongForGame(string Name) {
            string Hs = "0";

            string GameID = GetGameID(Name);

            Hs = GetHs(GameID);

            return Hs;
        }

        private static string GetHs(string Id) {
            HttpClient client = new HttpClient();
            var res = client.GetAsync(HttpUtility.UrlEncode(BasePath + GamePath + Id)).Result;
            string GameHTML = res.Content.ReadAsStringAsync().Result;

            return "";
        }

        private static string GetGameID(string Name) {
            string Id = "";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent","Chrome/51.0.2704.103"); // Defino un User-Agent o la Pagina no autoriza.
            var res = client.GetAsync(BasePath + "?q=" + Uri.EscapeDataString(Uri.EscapeDataString(Name))).Result;
            string GameHTML = res.Content.ReadAsStringAsync().Result;
            

            Id = GameHTML.Split(IdSearcher.Replace("[NOMBRE]", Name))[1].Split("\"")[0];

            return Id;
        }
    }
}
