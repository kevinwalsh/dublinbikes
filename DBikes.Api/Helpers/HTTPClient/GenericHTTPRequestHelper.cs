using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace DBikes.Api.Helpers.HTTPClient
{
    public static class GenericHTTPRequestHelper
    {
        public static string SendHttpRequest(string fullurl)
        {
            HttpWebRequest DBikesApiRequest = WebRequest.Create(
                   fullurl
               ) as HttpWebRequest;

            string responseString = "";
            using (HttpWebResponse response = DBikesApiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                responseString = reader.ReadToEnd();

                //  KW: was not in guide, but its a good idea to explicitly close response stream
                response.Close();
            }
            return responseString;
        }
    }
}