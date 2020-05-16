using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBikes.Api.Models.DBikesModels
{
    public class Position2
    {
        public double lat { get; set; }
        public double lng { get; set; }

    }
    public class BikeStationExact
    {
        public int number { get; set; }
        public string contract_name { get; set; }
        public string name { get; set; }
        public Position2 position { get; set; }
        public int available_bikes { get; set; }
        public int available_bike_stands { get; set; }
        public int bike_stands { get; set; }
        public string status { get; set; }
        public bool banking { get; set; }
        public bool bonus { get; set; }
        public long last_update { get; set; }
    }
}