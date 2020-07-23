using System.Threading.Tasks;

namespace DBikes.Api.Helpers.HTTPClient
{
    public class DublinBikesStaticHTTPClientHelper
    {
        string url = "https://developer.jcdecaux.com/rest/vls/stations/";

        public async Task<string> GetCityInfo(string city)
        {
            var o = await GenericHTTPRequestHelper.SendHttpRequest(url + city+".json");
            return o;
        }
    }
}