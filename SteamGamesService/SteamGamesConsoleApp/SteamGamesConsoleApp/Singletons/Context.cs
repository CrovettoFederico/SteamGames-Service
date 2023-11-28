using Gameloop.Vdf.Linq;
using Microsoft.Extensions.Configuration;
using SteamGamesConsoleApp.Models.SteamModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamGamesConsoleApp.Singletons {
    public static class Context {
        public static IConfiguration Configuration { get; set; }
        public static SharedConfig sharedConfig { get; set; }
        public static VProperty VDF_sharedConfig { get; set; }
        public static string SharedConfigOGJson{  get; set; }
    }
}
