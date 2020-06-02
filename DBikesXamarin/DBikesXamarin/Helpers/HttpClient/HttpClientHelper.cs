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

            var req = new HttpRequestMessage();
            req.RequestUri = new Uri(url);
            req.Headers.Add("Host", "localhost:51754");

            HttpResponseMessage response = await client.SendAsync(req);
            if (!response.IsSuccessStatusCode)
            {
                throw new System.Exception("KW HTTP EXCEPTION");
            }
            var myjson = await response.Content.ReadAsStringAsync();
            return myjson;
        }

    }
}
