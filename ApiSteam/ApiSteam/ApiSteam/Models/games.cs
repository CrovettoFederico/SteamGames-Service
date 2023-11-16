using System.Xml.Serialization;

namespace ApiSteam.Models {

    [XmlRoot(ElementName = "games")]
    public class games {

        [XmlElement(ElementName = "game")]
        public List<game> game { get; set; }
    }
}
