using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SteamGamesConsoleApp.Models.SteamModels {
    public class UserRoamingConfigStore
    {
        public Software Software { get; set; }
        public Dictionary<string, string> Web { get; set; }
        public Dictionary<string, JSClientStorage> JSClientStorage { get; set; }
    }
}
