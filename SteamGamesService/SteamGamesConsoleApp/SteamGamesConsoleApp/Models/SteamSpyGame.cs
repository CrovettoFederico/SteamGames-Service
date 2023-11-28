using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamGamesConsoleApp.Models {
    public class SteamSpyGame {
        public int appid { get; set; }
        public double average_2weeks { get; set; }
        public double average_forever { get; set; }
        public double ccu { get; set; }
        public string developer { get; set; }
        public string discount { get; set; }
        public string genre { get; set; }
        public string initialprice { get; set; }
        public string languages { get; set; }
        public double median_2weeks { get; set; }
        public double median_forever { get; set; }
        public string name { get; set; }
        public double negative { get; set; }
        public string owners { get; set; }
        public double positive { get; set; }
        public string price { get; set; }
        public string publisher { get; set; }
        public string score_rank { get; set; }
        public Dictionary<string, int> tags { get; set; }
        public double userscore { get; set; }
    }
}
