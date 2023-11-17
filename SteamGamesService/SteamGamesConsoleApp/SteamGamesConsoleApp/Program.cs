using Gameloop.Vdf.JsonConverter;
using Gameloop.Vdf;
using Gameloop.Vdf.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using SteamGamesConsoleApp.Models.SteamModels;
using SteamGamesConsoleApp.Singletons;

Console.WriteLine("Hello, World!");

SharedConfig sharedConfig =  VDFSingleton.GetSharedConfig();
var AppList = sharedConfig.UserRoamingConfigStore.Software.valve.Steam.GetAppList();
int lala = 2;