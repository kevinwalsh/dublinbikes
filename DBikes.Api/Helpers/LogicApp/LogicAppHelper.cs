using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DBikes.Api.Helpers.LogicApp
{
    public static class LogicAppHelper
    {
        public static void SendErrorToStorageAccount(string errtype, string errmsg, string srcfunction)
        {
            var logicappurl = "placeholder_url";
                            // Required message body format, wrapped in a "dberror" property
            object err = new
            {
                dberror = new
                {
                    PartitionKey = Guid.NewGuid().ToString(),
                    RowKey = Guid.NewGuid().ToString(),
                    errortype = errtype,
                    errormessage = errmsg,
                    sourcefunction = srcfunction
                }
            };

            var errjson = Newtonsoft.Json.JsonConvert.SerializeObject(err);
            var errjsonASCII = Encoding.ASCII.GetBytes(errjson);

            HttpWebRequest LogicAppRequest = WebRequest.Create(logicappurl) as HttpWebRequest;
            LogicAppRequest.Method = "POST";
            LogicAppRequest.ContentType = "application/json";
            LogicAppRequest.ContentLength = errjsonASCII.Length;

            var responseString = "";
            try
            {
                using (var stream = LogicAppRequest.GetRequestStream())
                {
                    stream.Write(errjsonASCII, 0, errjsonASCII.Length);             // encode request body as stream
                }
                var response = (HttpWebResponse)LogicAppRequest.GetResponse();
                responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception Occured");

            }
        }
    }
}
