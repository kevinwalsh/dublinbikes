using System;
using System.Collections.Generic;
using System.Text;

namespace DBikesXamarin.Models
{

    public static class DBikesSettings
    {
        public static string apiUrl { get; set; }
        public static string host { get; set; }
        public static string defaultCity { get; set; }
        public static int defaultSearchRadius { get; set; }
        public static int defaultHTTPPollTime { get; set; }
        public static int defaultHTTPTimeout { get; set; }


        public static void SetAppSettings(bool IsProduction)
        {
            if (IsProduction)
            {
                //  TODO fix production API to accept CORS from home IP
                DBikesSettings.apiUrl = "https://dublinbikesapi.azurewebsites.net";
                DBikesSettings.host = "dublinbikesapi.azurewebsites.net";
                DBikesSettings.defaultCity = "dublin";
                DBikesSettings.defaultSearchRadius = 1000;
                DBikesSettings.defaultHTTPPollTime = 60;
                DBikesSettings.defaultHTTPTimeout = 15;
            }
            else
            {
                DBikesSettings.apiUrl = "https://10.0.2.2:44303/api/DublinBikes";
                DBikesSettings.host = "localhost:44303";
                DBikesSettings.defaultCity = "dublin";
                DBikesSettings.defaultSearchRadius = 1000;
                DBikesSettings.defaultHTTPPollTime = 60;
                DBikesSettings.defaultHTTPTimeout = 15;
            }
        }

    }

}
