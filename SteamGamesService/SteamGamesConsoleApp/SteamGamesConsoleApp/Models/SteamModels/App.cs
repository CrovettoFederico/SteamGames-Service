using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamGamesConsoleApp.Models.SteamModels
{
    public class App
    {
        public List<Dictionary<string, string>> tags { get; set; }

        public string? hidden = "0";

        public List<SteamTag> GetTags()
        {
            var CurrentTags = new List<SteamTag>();

            tags.ForEach(x =>
            {
                CurrentTags.Add(new SteamTag() { Id = x.Keys.First(), Name = x.Values.First() });
            });
            return CurrentTags;
        }
    }
}
