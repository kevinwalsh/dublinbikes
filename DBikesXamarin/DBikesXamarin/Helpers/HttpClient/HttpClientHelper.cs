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
            client.Timeout=TimeSpan.FromSeconds(5);     // Max wait time

            HttpResponseMessage response;
            string myjson = "";
            try {
                var req = new HttpRequestMessage();
                req.RequestUri = new Uri(url);
                req.Headers.Add("Host", "localhost:51754");
                response = await client.SendAsync(req);

                if (!response.IsSuccessStatusCode)
                {
                    throw new System.Exception("KW HTTP EXCEPTION");
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

    }
}
