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
            using (HttpWebResponse response = DBikesApiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                responseString = reader.ReadToEnd();

                //  KW: was not in guide, but its a good idea to explicitly close response stream
                response.Close();
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