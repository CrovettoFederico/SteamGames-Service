using SteamGamesConsoleApp.Models.SteamModels;
using SteamGamesConsoleApp.Singletons;
using SteamGamesConsoleApp.Models;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Drawing.Text;
using SteamGamesConsoleApp;

var builder = new ConfigurationBuilder()
                .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true);
Context.Configuration = builder.Build();

Sincronizador.SincronizarLibreriaSteam();