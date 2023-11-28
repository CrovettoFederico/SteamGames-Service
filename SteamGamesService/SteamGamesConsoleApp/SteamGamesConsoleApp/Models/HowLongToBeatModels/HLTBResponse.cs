using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamGamesConsoleApp.Models.HowLongToBeatModels {
    public class HLTBResponse {
        public string description { get; set; }
        public double gameplayCompletionist { get; set; }
        public double gameplayMain { get; set; }
        public double gameplayMainExtra { get; set; }
        public int id { get; set; }
        public string imageUrl { get; set; }
        public string name { get; set; }
        public List<string> platforms { get; set; }
        public List<string> playableOn { get; set; }
        public string searchTerm { get; set; }
        public double similarity { get; set; }
        public List<List<string>> timeLabels { get; set; }
    }
}
