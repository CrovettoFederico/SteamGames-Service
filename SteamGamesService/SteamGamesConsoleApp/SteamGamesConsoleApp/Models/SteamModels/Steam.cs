using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamGamesConsoleApp.Models.SteamModels {
    public class Steam
    {
        public string SurveyDate { get; set; }
        public string SurveyDateVersion { get; set; }
        public string SteamDefaultDialog { get; set; }
        public string ShowScreenshotManager { get; set; }
        public string StartMenuShortcutCheck { get; set; }
        public string DesktopShortcutCheck { get; set; }
        public string SSAVersion { get; set; }
        public List<Dictionary<string, App>> apps { get; set; }
        public Friendsui friendsui { get; set; }
        
        public List<SteamApp> GetAppList()
        {
            List<SteamApp> AppList = new List<SteamApp>();
            apps.ForEach(x =>
            {
                try {
                    AppList.Add(new SteamApp() { Id = x.Keys.First(), Tags = x.Values.First().GetTags() });
                } catch (Exception e) {
                    AppList.Add(new SteamApp() {
                        Id = x.Keys.First(),
                        Tags = null
                    });
                }
            });
            return AppList;
        }
    }
}
