using Gameloop.Vdf.JsonConverter;
using Gameloop.Vdf.Linq;
using Gameloop.Vdf;
using Newtonsoft.Json;
using SteamGamesConsoleApp.Models.SteamModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SteamGamesConsoleApp.Singletons {
    static public class VDFSingleton {

        static string VDFPath = "D:\\Programar\\GitHub\\SteamGames-Service\\sharedconfig.vdf";
        static string ComasRegex = ",(?=(?:[^[\\]]*\\[[^[\\]]*\\])*[^[\\]]*\\])";
        static string TagsRegex = "\"tags\": {([^}]*)}";

        public static SharedConfig GetSharedConfig() {
            // Cargo archivo
            VProperty SharedConfigVDF = VdfConvert.Deserialize(File.ReadAllText(VDFPath));

            // Paso a Json
            var SharedConfigBadJson = SharedConfigVDF.ToJson(new VdfJsonConversionSettings {
                ObjectDuplicateKeyHandling = DuplicateKeyHandling.Ignore,
                ValueDuplicateKeyHandling = DuplicateKeyHandling.Ignore
            }).ToString();

            // Parseo a Listas y Objetos
            string aux = SharedConfigBadJson.Replace("\"apps\": {", "\"apps\": [\n\t{");
            string AppBrackets = aux.Replace("},\r\n        \"friendsui\":", "}\n\t],\r\n        \"friendsui\":");
            string AppsAndTagsBrackets = Regex.Replace(AppBrackets, TagsRegex, "\"tags\":[{$1}]");
            string SharedConfigJsonFixed = "{\n" + Regex.Replace(AppsAndTagsBrackets, ComasRegex, "},{") + "\n}";

            // Deserealizo Json fixeado
            SharedConfig sharedConfig = JsonConvert.DeserializeObject<SharedConfig>(SharedConfigJsonFixed);
            return sharedConfig;
        }

        public static bool WriteSharedConfig(SharedConfig config) {



            return false;
        }
    }
}
