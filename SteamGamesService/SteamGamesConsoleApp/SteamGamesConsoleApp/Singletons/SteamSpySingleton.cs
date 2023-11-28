using SteamGamesConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;

namespace SteamGamesConsoleApp.Singletons {
    public static class SteamSpySingleton {

        private static string SteamSpyUrl = Context.Configuration["SteamSpyUrl"];

        public static SteamSpyGame GetSteamSpyGame (int id) {
            try {
                using (var client = new HttpClient()) {
                    var res = client.GetAsync(SteamSpyUrl + id.ToString()).Result;
                    var Game = JsonConvert.DeserializeObject<SteamSpyGame>(res.Content.ReadAsStringAsync().Result);
                    return Game;
                }
            }catch (Exception ex) {
                return null;
            }
        }
    }
}
