using SteamGamesConsoleApp.Models.SteamModels;
using SteamGamesConsoleApp.Singletons;
using SteamGamesConsoleApp.Models;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

Process[] procs = Process.GetProcessesByName("Steam");

if (procs.Any()) {
    procs[0].Kill();
}

Thread.Sleep(1000);

var builder = new ConfigurationBuilder()
    .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true);
Context.Configuration = builder.Build();

Context.VDF_sharedConfig = VDFSingleton.GetSharedConfigVProperty();
Context.sharedConfig = VDFSingleton.GetSharedConfigObject(Context.VDF_sharedConfig);

List<SteamApp> AppList = Context.sharedConfig.UserLocalConfigStore.Software.valve.Steam.GetAppList().ToList();
List<SteamApp> AProcesar = AppList.Select(app => {
    SteamApp PaProcesar = null;
    if (app.Tags != null) {
        app.Tags.ForEach(tag => {
            if (tag.Name == "A Procesar")
                PaProcesar = app;
        });
    }
    return PaProcesar;
}).Where(app => app != null).ToList();


string RLId = "252950";
if (AProcesar != null && AProcesar.Count > 0) {
    AProcesar.ForEach(Juego => {
        SteamSpyGame InfoJuego = SteamSpySingleton.GetSteamSpyGame(int.Parse(RLId));
        var duracion = HLTBSingleton.ConseguirCuantoDura("Rocket League");
        var allapps = Context.sharedConfig.UserLocalConfigStore.Software.valve.Steam.apps.Where(x => x.ContainsKey(RLId)).FirstOrDefault();
        if (allapps != null) {
            var tags = allapps[RLId].tags;
            tags[0] = new Dictionary<string, string>() { { "0", "A Procesar" } };
        }
    });

    var Diff = VDFSingleton.WriteAppDiferences(Context.sharedConfig, Context.VDF_sharedConfig);

    
    Process.Start(new ProcessStartInfo {
        FileName = "explorer",
        Arguments = "steam://resetcollections",
        UseShellExecute = true
    });

    Console.WriteLine("Acepta en steam cagonnnnn");
    Console.Read();
}

/*
if (AProcesar != null) {
    AProcesar.ForEach(Juego => {
        SteamSpyGame InfoJuego = SteamSpySingleton.GetSteamSpyGame(int.Parse(Juego.Id));
        var duracion = HLTBSingleton.ConseguirCuantoDura(InfoJuego.name);
        var allapps = Context.sharedConfig.UserLocalConfigStore.Software.valve.Steam.apps.Where(x => x.ContainsKey(Juego.Id)).FirstOrDefault();
        if(allapps != null) {
            var tags = allapps[Juego.Id].tags;            
            tags[0] = new Dictionary<string, string>() { { "0", duracion._Duracion.ToString()} };
        }
    });
}
*/
//Process.Start("steam://resetcollections");

