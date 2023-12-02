using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamGamesConsoleApp.Singletons {
    public static class BackUpSecurity {
        public static void BackUpFile(string Origen, string Destino, string FileName) {
            try {
                int Archivos = Directory.GetFiles(Destino).Length;
                File.Copy(Origen + FileName, Destino + FileName + DateTime.Now.ToString("yyyy-MM-dd hh-mm") + "hs " + FileName + ".bkp_" + (Archivos + 1).ToString());

            }catch (Exception e) {
                throw new Exception("Error al intentar hacer BackUp del VDF.", e);
            }
        }
    }
}
