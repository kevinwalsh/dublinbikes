using DBikes.Api.Helpers.HTTPClient;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Http;
using System.Xml;

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

            string result = dbhelper.GetStation(id);
            return JsonConvert.DeserializeObject(result);
        }

        [HttpGet]
        [Route("GetAllStations")]
        public object GetAllStations()
        {
            DublinBikesHTTPClientHelper dbhelper = new DublinBikesHTTPClientHelper();
            string result = dbhelper.GetAllStations();
            return JsonConvert.DeserializeObject(result);
        }

        [HttpGet]
        [Route("GetStation_NoAPIKey")]
        public object GetStation_XML_NoAPIKey(int stationId)
        {
            DublinBikesHTTPClientHelper dbhelper = new DublinBikesHTTPClientHelper();
            var result = dbhelper.GetStation_NoAPIRequired(stationId);
            XmlDocument doc = new XmlDocument();            // returns XML, not JSON
            doc.LoadXml(result);
            // var responseObject = JsonConvert.SerializeXmlNode(doc);

            return doc;
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
