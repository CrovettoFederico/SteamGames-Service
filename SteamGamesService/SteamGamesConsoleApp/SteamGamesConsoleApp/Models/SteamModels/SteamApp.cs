using Gameloop.Vdf.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SteamGamesConsoleApp.Models.SteamModels {
    public class SteamApp {
        public string Id { get; set; }
        public List<SteamTag> Tags { get; set; }
        public string? Hidden = "0";

        public VProperty ToVProperty() {
            //property-> object -> property->objects->tags como propertys          

            //Tags = new List<SteamTag>() { new SteamTag { Id = "0", Name = "Dejados" }}; // Genero un cambio

            VObject TagsVO = new VObject();
            Tags.ForEach(tag => {
                TagsVO.Add(new VProperty(tag.Id, new VValue(tag.Name)));
            });

            VProperty TagsProp = new VProperty("tags", TagsVO);
            VProperty HiddenProp = new VProperty("hidden", new VValue(Hidden));
            VObject AppVObject = new VObject() {
                TagsProp,
                HiddenProp
            };

            VProperty AppProp = new VProperty(Id, AppVObject);




            return AppProp;
        }

        private VToken TagsToVToken() {
            VValue TagsVDF = new VValue(Tags);

            return TagsVDF;
        }
    }
}
