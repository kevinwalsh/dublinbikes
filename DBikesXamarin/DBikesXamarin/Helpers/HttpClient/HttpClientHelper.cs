using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DBikesXamarin
{
    static class HttpClientHelper
    {

        public async static Task<string> HttpGetRequest(string url)
        {
            HttpClient client = new HttpClient();
            client.Timeout=TimeSpan.FromSeconds(15);     // Max wait time

            HttpResponseMessage response;
            string myjson = "";
            try {
                var req = new HttpRequestMessage();
                req.RequestUri = new Uri(url);
                // req.Headers.Add("Host", "localhost:51754");
                req.Headers.Add("Host", "dublinbikesapi.azurewebsites.net");            // omit http & www for web addresses!

                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GenerateSimpleToken());
                response = await client.SendAsync(req);

                if (!response.IsSuccessStatusCode)
                {
                    throw new System.Exception("KW HTTP EXCEPTION");            // if 400/ bad request, doublecheck host headers
                }
                 myjson = await response.Content.ReadAsStringAsync();
            }
            
            catch (Exception exception)
            {
                                            // catch expected errors, handle scenario on UI when json returns null
                if (exception.Message == "Socket closed" || exception.Message == "Canceled")
                {
                    Console.WriteLine("KW: Socket/Server exception: couldn't retrieve response from server. \n " + exception);
                }
                else throw exception;
            }
            return myjson;
        }

        public static string GenerateSimpleToken()
        {
            var key = "dublinbikestoken";
            Int32 hash = 0;
            // var chararray = (key + DateTime.Today.ToString("yyyy-MM-dd")).ToCharArray();         //daily
            var chararray = (key + ((DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()) /1000000));        // round to 15mins

            for (var i = 0; i < chararray.Length; i++)
            {
                hash = ((hash << 5) - hash) + chararray[i];
                hash |= 0;
            }
            var tok = hash.ToString();
            return tok;
        }

    }
}
