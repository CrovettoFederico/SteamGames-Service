using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamGamesConsoleApp.Models.SteamModels {
    public class SteamApp {
        public string Id { get; set; }
        public List<SteamTag> Tags { get; set; }
    }
}
