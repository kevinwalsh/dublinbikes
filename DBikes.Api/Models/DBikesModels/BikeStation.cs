using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBikes.Api.Models.DBikesModels
{
    public class Position
    {
        public double lat { get; set; }
        public double lng { get; set; }

    }
    public class BikeStation
    {
        [JsonProperty(PropertyName = "number")]
        public int stationNumber { get; set; }
        [JsonProperty("contract_name")]
        public string contractName { get; set; }
        [JsonProperty("name")]
        public string stationName { get; set; }
        /*[JsonProperty("position/lat")]         // problems auto accessing child elements; created Position class
        string positionLat;
        [JsonProperty("position.lng")]
        string positionLong;
        */
        [JsonProperty("position")]
        public Position position;

        [JsonProperty("available_bikes")]
        public int available { get; set; }
        [JsonProperty("available_bike_stands")]
        public int free { get; set; }
        [JsonProperty("bike_stands")]
        public int total { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("banking")]
        public bool isBanking { get; set; }
        [JsonProperty("bonus")]
        public bool isBonus { get; set; }
        [JsonProperty("last_update")]
        //  [JsonConverter(typeof(JsonDateConverter))]
        //  [JsonConverter(typeof(JsonDateConverter2))]         //problems autoconverting unix millisec to datetime
        //public DateTime updatedAt { get; set; }
        long updatedAt { get; set; }

    }
}