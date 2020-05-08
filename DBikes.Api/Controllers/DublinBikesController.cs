using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public int GetStationById(int id)
        {
            //      http://localhost:51754/api/DublinBikes/GetStation/1
            return id;
        }

        [HttpGet]
        [Route("GetAllStations")]
        public List<int> GetAllStations()
        {
            var nums = new List<int>() { 1, 2, 3, 4, 5 };
            return nums;
        }

    }
}
