﻿using DBikes.Api.Helpers.APIKey;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace DBikes.Api.Helpers.HTTPClient
{
    /*
            Built with help from guide at:
            https://www.codeproject.com/Articles/1180283/How-to-Implement-OpenWeatherMap-API-in-ASP-NET-MVC

        Note:   using   System.NET.Http for network, while WEB.Http is for "the web"    
            -> "Web" = for server/ hosting apparently,  while "Net" is client/ consuming external APIs


        TODO: 
            -   Handle 403 errors     (incorrect API key)
            -   Handle 404 Errors     (Station num not found)
    */

    public class DublinBikesHTTPClientHelper
    {
         IApiKeyHelper akhelper = new FakeApiKeyHelper();
        // IApiKeyHelper akhelper = new ApiKeyHelper();

        string baseurl = "https://api.jcdecaux.com/vls/v1/stations";
        string locationparam = "&contract=dublin";

        private string BuildUrlParams( int? stationid = null)
        {
        /*  sample single:  https://api.jcdecaux.com/vls/v1/stations/{station_number}?contract={contract_name} 
            sample all in area  https://api.jcdecaux.com/vls/v1/stations?contract={contract_name}&apiKey={api_key}
        */

            string key = akhelper.GetApiKey("dublinbikes");
            var fullUrl = "";
            if (stationid!= null)
            {
                fullUrl += baseurl + "/" + stationid + "?apiKey=" + key + locationparam;
            }
            else
            {
                fullUrl += baseurl + "?apiKey=" + key + locationparam;
            }
            return fullUrl;
        }

        public object GetStation(int stationid)
        {
            HttpWebRequest DBikesApiRequest = WebRequest.Create(
                    BuildUrlParams(stationid)
                ) as HttpWebRequest;

            string responseString = "";
            using (HttpWebResponse response = DBikesApiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                responseString = reader.ReadToEnd();

                //  KW: was not in guide, but its a good idea to explicitly close response stream
                response.Close();
            }

            var responseObject = JsonConvert.DeserializeObject(responseString);
            return responseObject;
        }

        public object GetAllStations()
        {
            HttpWebRequest DBikesApiRequest = WebRequest.Create(
                    BuildUrlParams()
                ) as HttpWebRequest;

            string responseString = "";
            using (HttpWebResponse response = DBikesApiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                responseString = reader.ReadToEnd();

                //  KW: not in tut/e.g., but good idea to close response
                response.Close();
            }

            var responseObject = JsonConvert.DeserializeObject(responseString);
            return responseObject;
        }
        

        /*
            // https://www.codeproject.com/Articles/1180283/How-to-Implement-OpenWeatherMap-API-in-ASP-NET-MVC

         string apiKey = "Your API KEY";
        HttpWebRequest apiRequest =
        WebRequest.Create("http://api.openweathermap.org/data/2.5/weather?id=" +
        cities + "&appid=" + apiKey + "&units=metric") as HttpWebRequest;

        string apiResponse = "";
        using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
        {
            streamReader reader = new StreamReader(response.GetResponseStream());
            apiResponse = reader.ReadToEnd();
        }
        ResponseWeather rootObject = JsonConvert.DeserializeObject<ResponseWeather>(apiResponse);

         */

    }
}