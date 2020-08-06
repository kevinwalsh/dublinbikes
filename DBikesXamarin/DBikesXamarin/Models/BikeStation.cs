using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DBikesXamarin
{

    public class Position
    {
        public double lat { get; set; }
        public double lng { get; set; }

    }

    public class BikeStation
    {


        [JsonProperty(PropertyName = "stationNumber")]
        public int stationNumber { get; set; }
        [JsonProperty("contractName")]
        public string contractName { get; set; }
        [JsonProperty("stationName")]
        public string stationName { get; set; }
        /*[JsonProperty("position/lat")]         // problems auto accessing child elements; created Position class
        string positionLat;
        [JsonProperty("position.lng")]
        string positionLong;
        */
        [JsonProperty("position")]
        public Position position;

        [JsonProperty("bikes")]
        public int available { get; set; }
        [JsonProperty("freeStands")]
        public int free { get; set; }
        [JsonProperty("total")]
        public int total { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("isBanking")]
        public bool isBanking { get; set; }
        [JsonProperty("isBonus")]
        public bool isBonus { get; set; }
        [JsonProperty("updatedAt")]
        //  [JsonConverter(typeof(JsonDateConverter))]
        //  [JsonConverter(typeof(JsonDateConverter2))]         //problems autoconverting unix millisec to datetime
        //public DateTime updatedAt { get; set; }
        long updatedAt { get; set; }
    }
}
