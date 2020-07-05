using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;

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

            try { 
                using (HttpWebResponse response = DBikesApiRequest.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    responseString = reader.ReadToEnd();

                    //  KW: was not in guide, but its a good idea to explicitly close response stream
                    response.Close();
                }
            }
            catch (System.Net.WebException error) {
                var resp = (HttpWebResponse) error.Response;
                if (resp.StatusCode !=null && ((int)resp.StatusCode == 403 || (int)resp.StatusCode == 404))
                {
                    // want to throw 403/404 as error is from either client/3rdparty, not internal(500).
                    //  However, still don't want to send sensitive details (responseuri, etc) to client
                    //throw new System.Web.Http.HttpResponseException(resp.StatusCode);

                   // throw new HttpException(resp.StatusCode);     // no HTTP Exception in .NET Core; forums suggest using middleware
                }
                else
                {
                    throw error;                // default: rethrow original
                }
            }
            return responseString;
        }

        public static XmlDocument SendXMLRequest(string fullurl)        // didnt need in end but may use later
        {
            HttpWebRequest DBikesApiRequest = WebRequest.Create(fullurl) as HttpWebRequest;
            XmlDocument responseXML = new XmlDocument();
            using (HttpWebResponse response = DBikesApiRequest.GetResponse() as HttpWebResponse)
            {
                responseXML.Load(response.GetResponseStream());
                response.Close();
            }
            return responseXML ;
        }
    }
}