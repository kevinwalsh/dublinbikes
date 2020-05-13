using DBikes.Api.Helpers.HTTPClient;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Http;

namespace DBikes.Api.Controllers
{

    //    [Authorize]                 
    [RoutePrefix("api/DublinBikesStatic")]
    public class DublinBikesStaticController : ApiController
    {
        [HttpGet]
        [Route("GetStationsByCity")]
        public object GetStationByCity(string city)
        {
            DublinBikesStaticHTTPClientHelper dbSHelper = new DublinBikesStaticHTTPClientHelper();
            var result = dbSHelper.GetCityInfo(city);
            return JsonConvert.DeserializeObject(result);
        }
        

    }
}
