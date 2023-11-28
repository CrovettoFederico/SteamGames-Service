using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamGamesConsoleApp.Models.SteamModels {
    public class SteamTag {
        public string Id { get; set; }
        public string Name { get; set; }

        public Dictionary<string, string> ToDictionary() {
            return new Dictionary<string, string>() { { "Id", Id }, { "Name", Name } };
        }
    }
}
