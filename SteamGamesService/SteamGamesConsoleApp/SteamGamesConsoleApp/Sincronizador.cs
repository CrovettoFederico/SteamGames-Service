using Microsoft.Extensions.Configuration;
using SteamGamesConsoleApp.Models.SteamModels;
using SteamGamesConsoleApp.Models;
using SteamGamesConsoleApp.Singletons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamGamesConsoleApp {
    public static class Sincronizador {

        private static string SteamIgnoreID = Context.Configuration["SteamIgnoreID"];

        public static void SincronizarLibreriaSteam() {
            Process[] procs = Process.GetProcessesByName("Steam");

            if (procs.Any()) {
                procs[0].Kill();
            }

            Thread.Sleep(1000);
                       
            Context.VDF_sharedConfig = VDFSingleton.GetSharedConfigVProperty();
            Context.sharedConfig = VDFSingleton.GetSharedConfigObject(Context.VDF_sharedConfig);

            List<SteamApp> AppList = Context.sharedConfig.UserLocalConfigStore.Software.valve.Steam.GetAppList().ToList();
            List<SteamApp> AProcesar = AppList.Select(app => {
                SteamApp PaProcesar = null;
                if (app.Tags != null && app.Id != SteamIgnoreID) {
                    app.Tags.ForEach(tag => {
                        if (tag.Name == "A Procesar")
                            PaProcesar = app;
                    });
                }
                return PaProcesar;
            }).Where(app => app != null).ToList();



            if (AProcesar != null && AProcesar.Count > 0) {
                try {
                    ExcelSingleton.AbrirExcel();

                    AProcesar.ForEach(Juego => {
                        SteamSpyGame InfoJuego = SteamSpySingleton.GetSteamSpyGame(int.Parse(Juego.Id));
                        var duracion = HLTBSingleton.ConseguirCuantoDura(InfoJuego.name);
                        var allapps = Context.sharedConfig.UserLocalConfigStore.Software.valve.Steam.apps.Where(x => x.ContainsKey(Juego.Id)).FirstOrDefault();
                        if (allapps != null) {
                            var tags = allapps[Juego.Id].tags;
                            tags[0] = new Dictionary<string, string>() { { "0", duracion._Duracion.ToString() } };
                            ExcelSingleton.EscribirExcel(duracion);
                        }
                    });

                    ExcelSingleton.GuardarExcel();
                    ExcelSingleton.CerrarExcel();
                    var Diff = VDFSingleton.WriteAppDiferences(Context.sharedConfig, Context.VDF_sharedConfig);


                    Process.Start(new ProcessStartInfo {
                        FileName = "explorer",
                        Arguments = "steam://resetcollections",
                        UseShellExecute = true
                    });

                    Console.WriteLine("Acepta en steam cagonnnnn");
                    Console.Read();
                } catch (Exception e) {
                    Console.WriteLine("Hubo un error \n");
                    Console.WriteLine(e.ToString());
                    Console.Read();
                }
            }
        }

    }
}
