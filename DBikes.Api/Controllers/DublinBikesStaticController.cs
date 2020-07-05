//using DBikes.Api.Filters;
using DBikes.Api.Helpers.HTTPClient;
using DBikes.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace DBikes.Api.Controllers
{
    // [SimpleAuthentication]
    //    [Authorize]                 
    [Route("api/DublinBikesStatic")]
    public class DublinBikesStaticController : Controller
    {
        [HttpGet]
        [Route("GetStationsByCity")]
        public object GetStationByCity(string city)
        {
            DublinBikesStaticHTTPClientHelper dbSHelper = new DublinBikesStaticHTTPClientHelper();
            var result = dbSHelper.GetCityInfo(city);
            return JsonConvert.DeserializeObject(result);
        }

        [HttpGet]
        [Route("GetCities")]
        //public List<CityEnum> GetCities()
        public List<string> GetCitites()
        {
            // var enumvals = System.Enum.GetValues(typeof(CityEnum)).Cast<CityEnum>().ToList(); //gives NUMs not names
            var types = System.Enum.GetNames(typeof(CityEnum)).ToList();
            return types;
        }

        [HttpGet]
        [Route("SelectCity")]
        public CityEnum SelectCity(CityEnum city)
        {
            return city;      // inputs as string val (due to SwaggerConfig option); returns as corresponding number
        }

        [HttpGet]
        [Route("SearchCity")]
        public CityEnum SelectCity(string s)
        {
            CityEnum c;
            if(!System.Enum.TryParse(s, true, out c))
            {
                throw new System.Exception("CityEnumException: no matching city found");
            }
            return c;
         }

    }
}
