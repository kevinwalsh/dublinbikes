using System;
using System.Net.Http;
using System.Threading.Tasks;
using DBikesXamarin.Models;

namespace DBikesXamarin
{
    static class HttpClientHelper
    {
        // Bypass SSL Certs for HTTPS with no cert
                //  https://stackoverflow.com/questions/28629989/ignore-ssl-certificate-errors-in-xamarin-forms-pcl,        Andre's answer
        private static HttpClient GenerateHttpClient(bool bypassCertificate)
        {
            HttpClient client;
            if (bypassCertificate == true)
            {
                 client = new HttpClient(
                    new HttpClientHandler()
                    {   ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                        {   
                            return true;        //bypass failed SSL Cert authentication (Security risk, DO NOT do in production, or with sensitive data!)
                        },
                    }, false        // disposeHandler
                );
            }
            else { client = new HttpClient(); }
            return client;
        }

        public async static Task<string> HttpGetRequest(string url)
        {
            HttpClient client = GenerateHttpClient(true);                   //  enable/disable SSL certificate bypass.          *** N.B. SECURITY RISK! ***
            client.Timeout=TimeSpan.FromSeconds(DBikesSettings.defaultHTTPTimeout);

            HttpResponseMessage response;
            string myjson = "";
            try {
                var req = new HttpRequestMessage();
                req.RequestUri = new Uri(url);
                req.Headers.Add("Host", DBikesSettings.host);
                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GenerateSimpleToken());
                response = await client.SendAsync(req);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("KW HTTP EXCEPTION: response = " + response);
                    if (response.ReasonPhrase == "Site Disabled")
                    {
                        throw new System.Exception("Site Disabled");            // 403, "site disabled", likely the Azure API is down/stopped
                    }
                    else
                    {
                        throw new System.Exception("KW HTTP EXCEPTION");            // if 400/ bad request, doublecheck host headers
                    }
                }
                 myjson = await response.Content.ReadAsStringAsync();
            }
            
            catch (Exception exception)
            {
                                            // catch expected errors, handle scenario on UI when json returns null
                if (exception.Message == "Socket closed"
                    || exception.Message == "Canceled"
                    || exception.Message == "Site Disabled" ||
                    exception.Message.Contains("Unable to resolve host")
                    )
                {
                    Console.WriteLine("KW: Socket/Server exception: couldn't retrieve response from server. \n " + exception);
                }
                else throw exception;
            }
            return myjson;
        }

        public static string GenerateSimpleToken()
        {
            var key = DBikesSettings.AuthTokenKey;
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
