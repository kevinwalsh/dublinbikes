using DBikes.Api.Helpers.APIKey;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Xml;

namespace DBikes.Api.Helpers.HTTPClient
{
    public class DublinBikesStaticHTTPClientHelper
    {
        string url = "https://developer.jcdecaux.com/rest/vls/stations/";

        public string GetCityInfo(string city)
        {
            var o = GenericHTTPRequestHelper.SendHttpRequest(url + city+".json");
            return o;
        }
    }
}