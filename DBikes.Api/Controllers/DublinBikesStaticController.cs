//using DBikes.Api.Filters;
using DBikes.Api.Helpers.HTTPClient;
using DBikes.Api.Models;
using DBikes.Api.Models.DBikesModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBikes.Api.Controllers
{
    // [SimpleAuthentication]
    //    [Authorize]                 
    [Route("api/DublinBikesStatic")]
    public class DublinBikesStaticController : Controller

        /*
         TODO constructor like in other controller; memcache etc
         */
    {
        [HttpGet]
        [Route("GetStationsByCity")]
        public async Task<IActionResult> GetStationByCity(string city)
        {
            DublinBikesStaticHTTPClientHelper dbSHelper = new DublinBikesStaticHTTPClientHelper();
            var result = await dbSHelper.GetCityInfo(city);       // TODO FIX: 3rdparty crashes/ returns 500 if fake city requested
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(JsonConvert.DeserializeObject<List<BikeStation>>(result));
                                        // casting as BikeStation for now, though data contains no availability info/etc
        }

        [HttpGet]
        [Route("GetCities")]
        //public List<CityEnum> GetCities()
        public async Task<IActionResult> GetCitites()
        {
            // var enumvals = System.Enum.GetValues(typeof(CityEnum)).Cast<CityEnum>().ToList(); //gives NUMs not names
            var types = System.Enum.GetNames(typeof(CityEnum)).ToList();
            return Ok(types);
        }

        [HttpGet]
        [Route("SelectCity")]
        public async Task<IActionResult> SelectCity(CityEnum city)
        {
            return Ok(city);      // inputs as string val (due to SwaggerConfig option); returns as corresponding number
        }

        [HttpGet]
        [Route("SearchCity")]
        public async Task<IActionResult> SelectCity(string s)    // not very useful FN; (a) shouldnt return an exception; (b) currently only returns FULL matches
        {
            CityEnum c;
            if(!System.Enum.TryParse(s, true, out c))
            {
                throw new System.Exception("CityEnumException: no matching city found");
                return BadRequest();
            }
            return Ok(c);
         }

    }
}
