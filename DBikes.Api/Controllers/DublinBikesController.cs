using DBikes.Api.Helpers.HTTPClient;
using System.Collections.Generic;
using System.Web.Http;

namespace DBikes.Api.Controllers
{

    //    [Authorize]                 
    [RoutePrefix("api/DublinBikes")]
    public class DublinBikesController : ApiController
    {

        /*        [HttpPost]
                [Route("PostInteger")]
                public int PostInteger(int postednum)
                {
                    return postednum;
                }
        */

        [HttpGet]
        [Route("GetStation/{id}")]
        public object GetStationById(int id)
        {
            DublinBikesHTTPClientHelper dbhelper = new DublinBikesHTTPClientHelper();
            // var resSerialized = dbhelper.GetStation(id);
            // var resObj = JsonConvert.DeserializeObject(resSerialized);       //EDIT: doing on helper
            // return resObj;

            var result = dbhelper.GetStation(id);
            return result;
        }

        [HttpGet]
        [Route("GetAllStations")]
        public object GetAllStations()
        {
            DublinBikesHTTPClientHelper dbhelper = new DublinBikesHTTPClientHelper();
            var result = dbhelper.GetAllStations();
            return result;
        }

        [HttpGet]
        [Route("GetIntList")]
        public List<int> GetIntList()
        {
            var nums = new List<int>() { 1, 2, 3, 4, 5 };
            return nums;
        }

    }
}
