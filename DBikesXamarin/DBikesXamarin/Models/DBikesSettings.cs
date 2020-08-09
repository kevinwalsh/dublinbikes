using System;
using System.Collections.Generic;
using System.Text;

namespace DBikesXamarin.Models
{

    public static class DBikesSettings
    {
        public static string apiUrl { get; set; }
        public static string host { get; set; }
        public static string AuthTokenKey { get; set; }
        public static string defaultCity { get; set; }
        public static int defaultSearchRadius { get; set; }
        public static int defaultHTTPPollTime { get; set; }
        public static int defaultHTTPTimeout { get; set; }
        public static int disablePollingAfter { get; set; }
        public static int lowBikeThreshold { get; set; }

        public static void SetAppSettings(bool IsProduction)
        {
            if (IsProduction)
            {
                //  TODO fix production API to accept CORS from home IP
                DBikesSettings.apiUrl = "https://dublinbikesapi.azurewebsites.net";
                DBikesSettings.host = "dublinbikesapi.azurewebsites.net";
                DBikesSettings.AuthTokenKey = "dublinbikestoken";
                DBikesSettings.defaultCity = "dublin";
                DBikesSettings.defaultSearchRadius = 1000;
                DBikesSettings.defaultHTTPPollTime = 60;
                DBikesSettings.disablePollingAfter = 900;
                DBikesSettings.defaultHTTPTimeout = 15;
                DBikesSettings.lowBikeThreshold = 3;
            }
            else
            {
                DBikesSettings.apiUrl = "https://10.0.2.2:44303/api/DublinBikes";
                DBikesSettings.host = "localhost:44303";
                DBikesSettings.AuthTokenKey = "dublinbikestoken";
                DBikesSettings.defaultCity = "dublin";
                DBikesSettings.defaultSearchRadius = 500;
                DBikesSettings.defaultHTTPPollTime = 30;
                DBikesSettings.disablePollingAfter = 900;
                DBikesSettings.defaultHTTPTimeout = 15;
                DBikesSettings.lowBikeThreshold = 5;
            }
        }

    }

}
