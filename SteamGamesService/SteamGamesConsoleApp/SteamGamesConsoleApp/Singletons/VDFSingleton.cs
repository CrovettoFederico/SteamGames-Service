using Gameloop.Vdf.JsonConverter;
using Gameloop.Vdf.Linq;
using Gameloop.Vdf;
using Newtonsoft.Json;
using SteamGamesConsoleApp.Models.SteamModels;
using System.Text.RegularExpressions;

namespace SteamGamesConsoleApp.Singletons {
    static public class VDFSingleton {

        static readonly string VDFFileName = Context.Configuration["VDFFileName"];
        static readonly string VDFPath = Context.Configuration["VDFPath"];
        static readonly string ComasRegex = ",(?=(?:[^[\\]]*\\[[^[\\]]*\\])*[^[\\]]*\\])";
        static readonly string TagsRegex = "\"tags\": {([^}]*)}";

    
        public static VProperty GetSharedConfigVProperty() {
            var FileText = File.ReadAllText(VDFPath + VDFFileName);
            VProperty SharedConfigVDF = VdfConvert.Deserialize(File.ReadAllText(VDFPath + VDFFileName));
            return SharedConfigVDF;
        }

        public static SharedConfig GetSharedConfigObject(VProperty VDF) {
            // Paso a Json
            string SharedConfigJson = GetSharedConfigParsedJson(VDF);

            // Deserealizo Json fixeado
            SharedConfig sharedConfig = JsonConvert.DeserializeObject<SharedConfig>(SharedConfigJson);
            return sharedConfig;
        }
                
        public static string GetSharedConfigParsedJson(VProperty VDF) {
            var SharedConfigBadJson = GetSharedConfigOriginalJson(VDF);

            // Parseo a Listas y Objetos
            string aux = SharedConfigBadJson.Replace("\"apps\": {", "\"apps\": [\n\t{");
            string AppBrackets = aux.Replace("},\r\n        \"friendsui\":", "}\n\t],\r\n        \"friendsui\":");
            string AppsAndTagsBrackets = Regex.Replace(AppBrackets, TagsRegex, "\"tags\":[{$1}]");
            string SharedConfigJsonFixed = "{\n" + Regex.Replace(AppsAndTagsBrackets, ComasRegex, "},{") + "\n}";
            return SharedConfigJsonFixed;
        }

        public static string GetSharedConfigOriginalJson(VProperty VDF ) {
            return VDF.ToJson(new VdfJsonConversionSettings {
                ObjectDuplicateKeyHandling = DuplicateKeyHandling.Ignore,
                ValueDuplicateKeyHandling = DuplicateKeyHandling.Ignore
            }).ToString();
        }

        /// <summary>
        /// Escribe en objeto VDF las diferencias en el nodo "App" que encuentre en relacion con su objeto C# pasado y devuelve un nuevo VDF modificado (Deja intacto el original).
        /// </summary>
        /// <param name="config">Shared Config con las diferencias que queremos impactar</param>
        /// <param name="vdf">Objeto VDF de referencia.</param>
        /// <returns>Objeto VDF ya modificado</returns>
        public static SharedConfig WriteAppDiferences(SharedConfig NewConfig, VProperty RefVDF) {
           
            SharedConfig RefConfig = GetSharedConfigObject(RefVDF);
            VProperty NewVDF = (VProperty)RefVDF.DeepClone();
            List<SteamApp> Differences = GetAppDifferences(NewConfig, RefConfig);
            Differences.ForEach(diff => {
                WriteVDFAppDifferences(NewVDF, diff);
            });

            // Una vez escrito los cambios, lo vuelvo a leer y lo traigo.
            return GetSharedConfigObject(GetSharedConfigVProperty());
        }

        private static List<SteamApp> GetAppDifferences(SharedConfig NewConfig, SharedConfig RefConfig) {
            List<SteamApp> NewSteamApps = NewConfig.UserLocalConfigStore.Software.valve.Steam.GetAppList();
            List<SteamApp> RefSteamApps = RefConfig.UserLocalConfigStore.Software.valve.Steam.GetAppList();

            List<SteamApp> Differences = new List<SteamApp>();

            var diff = new List<SteamApp>();
            NewSteamApps.ForEach(NewApp => {
                if (NewApp.Tags != null) { 
                    var RefApp = RefSteamApps.Where(refApp => refApp.Id == NewApp.Id).First();
                    if (RefApp.Tags != null) {
                        if (RefApp.Tags.Count != NewApp.Tags.Count)
                            Differences.Add(NewApp);
                        else {
                            for (int i = 0; i < NewApp.Tags.Count; i++) {
                                if (NewApp.Tags[i].Name != RefApp.Tags[i].Name)
                                    Differences.Add(NewApp);
                            }
                        }
                    }                    
                }
            });
            

            /*
            List<SteamApp> Differences = NewSteamApps.Where(NewSteamApp => {
                return RefSteamApps.Where(
                    RefSteamApp => {
                        bool DiffApp;
                        try {
                            DiffApp = RefSteamApp.Id == NewSteamApp.Id && !NewSteamApp.Tags.Where(NewTag => {
                                bool MismosTags = RefSteamApp.Tags.Where(RefTag => {
                                    bool MismoTag = (RefTag.Id == NewTag.Id && RefTag.Name == NewTag.Name);
                                    return MismoTag;
                                }).ToList().Any();
                                return MismosTags;
                            }).ToList().Any();
                        }catch(Exception E) {
                            DiffApp = RefSteamApp.Id == NewSteamApp.Id && NewSteamApp.Tags != RefSteamApp.Tags;
                        }
                        return DiffApp;
                    }).ToList().Any();
            }).ToList();
            */
            return Differences;
        }

        /// <summary>
        /// Escribe el NewVDF en disco con la diferencia enviada.
        /// </summary>
        /// <param name="NewVDF">VDF A ser modificado y escrito</param>
        /// <param name="Difference">Diferencia a añadir al NewVDF</param>
        private static void WriteVDFAppDifferences(VProperty NewVDF , SteamApp Difference) {
            VProperty NewApp = Difference.ToVProperty();

            NewVDF.Value["Software"]["valve"]["Steam"]["apps"][NewApp.Key] = NewApp.Value;
            //NewVDF.Value["Software"]["valve"]["Steam"]["apps"].Append(NewVDFApp);

            
            using (TextWriter writer = File.CreateText(VDFPath + VDFFileName)) {
                // Serialize the settings object to VDF and write to the file
                var vdfSerializer = new VdfSerializer();
                vdfSerializer.Serialize(writer, NewVDF);
            }
        }
    }
}
