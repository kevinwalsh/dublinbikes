using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace DBikes.Api.Models.DBikesModels
{
    //[Serializable()]
    [XmlRoot(ElementName ="station")]
    public class BikeStationBasic
    {
        [XmlElement("total")]
        public int total { get; set; }
        [XmlElement("available")]
        public int available { get; set; }
        [XmlElement("free")]
        public int free { get; set; }
        [XmlElement("open")]
        public bool stationOpen { get; set; }
        [XmlElement("ticket")]
        public int ticket{ get; set; }   // unsure what this represents, but model wont work without 1:1 elements
        [XmlElement("connected")]
        public bool stationConnected { get; set; }
        [XmlElement("updated")]
        public long updatedAt { get; set; }
        
    }
}