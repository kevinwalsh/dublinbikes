using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBikes.CoreApi.SettingsOptions
{
    public class MySettingsOptions
    {
        public int SwaggerOpenApiVersion { get; set; }
        public string MyApiVersion { get; set; }
        public string Env { get; set; }
        public string[] AllowedUrls { get; set; }
        public string AuthTokenKey { get; set; }
        public string DefaultCity { get; set; }
        public int DefaultSearchRadius { get; set; }
        public int DefaultCacheLifetime { get; set; }

    }
}
