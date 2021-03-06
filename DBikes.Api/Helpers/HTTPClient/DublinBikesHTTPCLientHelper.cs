﻿using DBikes.Api.Helpers.APIKey;
using System.Threading.Tasks;
using System.Xml;

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
        // IApiKeyHelper akhelper = new FakeApiKeyHelper();
         IApiKeyHelper akhelper = new ApiKeyHelper();

        string baseurl = "https://api.jcdecaux.com/vls/v1/stations";
        string defaultcity = "";

        public DublinBikesHTTPClientHelper(
            string defaultCity
            )
        {
            this.defaultcity= defaultCity;
        }

        // TODO: change BuildUrlParams to a builder pattern
        private string BuildUrlParams(string city, int? stationid = null)
        {
        /*  sample single:  https://api.jcdecaux.com/vls/v1/stations/{station_number}?contract={contract_name} 
            sample all in area  https://api.jcdecaux.com/vls/v1/stations?contract={contract_name}&apiKey={api_key}
            XML: no api reqd, but no stationid in results  http://www.dublinbikes.ie/service/stationdetails/dublin/41
        */

            string key = akhelper.GetApiKey("dublinbikes");
            var fullUrl = "";
            if (stationid!= null)
            {
                fullUrl += baseurl + "/" + stationid + "?apiKey=" + key + "&contract=" + city;
            }
            else
            {
                fullUrl += baseurl + "?apiKey=" + key + "&contract=" + city;
            }
            return fullUrl;
        }

        public async Task<string> GetStation(string city, int stationid)
        {
            var url = BuildUrlParams(city, stationid);
            var responseString = await GenericHTTPRequestHelper.SendHttpRequest(url);
            return responseString;
        }

        public async Task<string> GetAllStations()
        {
            var url = BuildUrlParams(defaultcity);
            var responseString = await GenericHTTPRequestHelper.SendHttpRequest(url);
            return responseString;
        }
        public async Task<string> GetAllStations(string city)
        {
            var url = BuildUrlParams(city);
            var responseString = await GenericHTTPRequestHelper.SendHttpRequest(url);
            return responseString;
        }

        public async Task<string> GetStation_NoAPIRequired(int stationId)
        {
            var url = "http://www.dublinbikes.ie/service/stationdetails/dublin/" + stationId;
            var responseString = await GenericHTTPRequestHelper.SendHttpRequest(url);
            return responseString;
        }

        public XmlDocument GetStationXML_NoAPIRequired(int stationId)
        {
            var url = "http://www.dublinbikes.ie/service/stationdetails/dublin/" + stationId;
            var responsexml= GenericHTTPRequestHelper.SendXMLRequest(url);
            var stationxml = responsexml.DocumentElement.SelectSingleNode("available"); // station IS "base elem"
            return responsexml;
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