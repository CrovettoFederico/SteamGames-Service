using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamGamesConsoleApp.Singletons {
    public static class BackUpSecurity {
        public static void BackUpFile(string Path, string FileName) {
            File.Copy(Path, Path.Replace(FileName, DateTime.Now.ToString("yyyy-MM-dd hh-mm")+ "hs " + FileName + ".bkp"));
        }
    }
}
