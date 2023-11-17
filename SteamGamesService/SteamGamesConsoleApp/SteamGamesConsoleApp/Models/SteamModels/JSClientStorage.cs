using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamGamesConsoleApp.Models.SteamModels {
    public class JSClientStorage {
        [JsonProperty("JSClientStorage")]
        public List<Dictionary<string,string>> JsClientStorage { get; set; }
    }
}
