using System.Xml.Serialization;

namespace ApiSteam.Models {
    [XmlRoot(ElementName = "gamesList")]
    public class gamesList {
        [XmlElement(ElementName = "steamID64")]
        public double steamID64 { get; set; }

        [XmlElement(ElementName = "steamID")]
        public string steamID { get; set;}

        [XmlElement(ElementName = "games")]
        public games games { get; set;}

    }
}
