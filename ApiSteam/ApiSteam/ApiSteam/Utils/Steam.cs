using ApiSteam.Models;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ApiSteam.Utils {
    public static class Steam {

        public static gamesList GetGamesList(string Path) {
            HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri(OwnedGamesPath);
            var res = client.GetAsync(Path).Result;
            string GamesXML = res.Content.ReadAsStringAsync().Result;
            gamesList Games = new gamesList();

            XDocument xmlDoc = XDocument.Parse(GamesXML);
            XElement node1Element = xmlDoc.Descendants("gamesList").FirstOrDefault();
            XElement cdataValue = node1Element.Descendants("games").FirstOrDefault();

            XmlSerializer serializer = new XmlSerializer(typeof(gamesList));
            using (StringReader reader = new StringReader(GamesXML)) {
                Games = (gamesList)serializer.Deserialize(reader);
            }

            return Games;
        }

    }
}
