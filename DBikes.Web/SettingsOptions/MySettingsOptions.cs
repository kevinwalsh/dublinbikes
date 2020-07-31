using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBikes.Web.SettingsOptions
{
    public class MySettingsOptions
    {
        public string Env { get; set; }
        public string ApiUrl { get; set; }
        public string ClientUrl { get; set; }
        public string  DefaultCity { get; set; }
        public int DefaultSearchRadius { get; set; }
        public int DefaultHTTPPollTime { get; set; }

    }
}
