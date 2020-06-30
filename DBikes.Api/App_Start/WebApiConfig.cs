using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace DBikes.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // KW fixing a "self-referencing loop" error caused by the interlinking talbes,
            // as per stack overflow https://stackoverflow.com/questions/7397207/json-net-error-self-referencing-loop-detected-for-type
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
                = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            // KW CORS
            var corsOrigins= "http://localhost:60724,http://dublinbikesweb.azurewebsites.net";
            EnableCorsAttribute cors = new EnableCorsAttribute(corsOrigins, "*", "*"); 
            config.EnableCors(cors);

        }
    }
}
