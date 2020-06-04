﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBikesXamarin.Helpers
{
    static class DBikesHttpHelper
    {
        static string baseURL ="http://10.0.2.2:51754/api/DublinBikes";
                        // Alias to Loopback to localhost IP; android emulators can't directly access localhost
                                //https://developer.android.com/studio/run/emulator-networking
        static int StationRadiusMetres = 500;

        public static async Task<List<BikeStation>> GetAllStations()
        {
            var url = baseURL + "/GetAllStations";
            var myjson = await HttpClientHelper.HttpGetRequest(url);
            var stations = JsonConvert.DeserializeObject<List<BikeStation>>(myjson);
            return stations;
        }
        public static async Task<List<BikeStation>> GetStation(int StationId)
        {
            var url = baseURL + "/GetStation/"+StationId;
            var myjson = await HttpClientHelper.HttpGetRequest(url);
            var stations = JsonConvert.DeserializeObject<List<BikeStation>>(myjson);
            return stations;
        }
        public static async Task<List<BikeStation>> GetNearbyStations(int StationId)
        {
            var url = baseURL + "/GetStationsWithinMetres/"+StationId+"/"+StationRadiusMetres;
            var myjson = await HttpClientHelper.HttpGetRequest(url);
            var stations = JsonConvert.DeserializeObject<List<BikeStation>>(myjson);
            return stations;
        }
    }
}