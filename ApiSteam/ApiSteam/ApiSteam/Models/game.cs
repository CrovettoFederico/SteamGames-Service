using System.Globalization;
using System.Xml.Serialization;

namespace ApiSteam.Models {
    [XmlRoot(ElementName = "game")]
    public class game {
        public game() {
            HowLong = 27;
        }

        [XmlElement(ElementName = "appID")]
        public int appID { get; set; }

        [XmlElement(ElementName = "name")]
        public string name { get; set; }

        [XmlElement(ElementName = "logo")]
        public string logo { get; set; }

        [XmlElement(ElementName = "storeLink")]
        public string storeLink { get; set; }

        [XmlIgnore]
        public decimal hoursLast2Weeks { get; set; }
        [XmlElement(ElementName = "hoursLast2Weeks")]
        public string hoursLast2WeeksString {
            get {
                return hoursLast2Weeks.ToString();
            }
            set {
                NumberFormatInfo NumberFormatInfo = new NumberFormatInfo();
                NumberFormatInfo.NumberDecimalSeparator = ".";
                hoursLast2Weeks = decimal.Parse(value.Replace(",", ""), NumberFormatInfo);
            }
        }

        [XmlIgnore]
        public decimal hoursOnRecord { get; set; }
        [XmlElement(ElementName = "hoursOnRecord")]
        public string hoursOnRecordString { 
            get {
                return hoursOnRecord.ToString();
            }
            set {
                NumberFormatInfo NumberFormatInfo = new NumberFormatInfo();
                NumberFormatInfo.NumberDecimalSeparator = ".";
                hoursOnRecord = decimal.Parse(value.Replace(",",""), NumberFormatInfo);
            }
        }

        [XmlElement(ElementName = "statsLink")]
        public string statsLink { get; set; }

        [XmlElement(ElementName = "globalStatsLink")]
        public string globalStatsLink { get; set; }

        [XmlIgnore]
        public int HowLong { get; set; }
    }
}
