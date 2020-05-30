using DBikes.Api.Helpers.GPSHelper;
using DBikes.Api.Helpers.HTTPClient;
using DBikes.Api.Models.DBikesModels;
using DBikes.Api.Providers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;

namespace DBikes.Api.Controllers
{

    //    [Authorize]                 
    [RoutePrefix("api/DublinBikes")]
    public class DublinBikesController : ApiController
    {
        private DBikesMemoryCache cache;
        private DublinBikesHTTPClientHelper dbhelper;

        public DublinBikesController()
        {
            cache = new DBikesMemoryCache();
            dbhelper = new DublinBikesHTTPClientHelper();
        }

        /*        [HttpPost]
                [Route("PostInteger")]
                public int PostInteger(int postednum)
                {
                    return postednum;
                }
        */

        /// <summary>
        /// Return a single station.
        /// TODO: convert "last_update" to DateTime
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetStation/{id}")]
        public object GetStationById(int id=11)
        {
             List<BikeStation> stations = (List<BikeStation>) cache.CheckCache("dublin");
            BikeStation station = null;
            if (stations != null)
            {
                station = stations.Single(x => x.stationNumber == id);
            }
            else
            {
                string result = dbhelper.GetStation(id);
                station = JsonConvert.DeserializeObject<BikeStation>(result);
            }
            List<BikeStation> stationAsList = new List<BikeStation>() { station };
            return stationAsList;
        }

        /// <summary>
        /// Debug function to return model with properties exactly matching parameters of expected JSON.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetStationExactModel/{id}")]
        public object GetStationExactModel(int id=11)
        {
            string result = dbhelper.GetStation(id);
            BikeStationExact bse = JsonConvert.DeserializeObject<BikeStationExact>(result);
            return bse;
        }

        [HttpGet]
        [Route("GetAllStations")]
        public object GetAllStations()
        {
            List<BikeStation> stations = (List<BikeStation>) cache.CheckCache("dublin");
            if (stations == null)
            {
                string result = dbhelper.GetAllStations();
                stations = JsonConvert.DeserializeObject<List<BikeStation>>(result).ToList();
                cache.AddToCache("dublin", stations);
            }
            
            //            return stations;
            return stations.Where(x=>x.stationNumber < 15).OrderBy(x=>x.stationNumber);     
                                //  filter 110-item list to reduce response size
        }

        [HttpGet]
        [Route("GetStationsWithinMetres/{id}/{metres}")]
        public object GetStationsWithinMetres(int id, int metres)
        {
            List<BikeStation> stations = (List<BikeStation>)cache.CheckCache("dublin");
            if (stations == null)
            {
                string result = dbhelper.GetAllStations();
                stations = JsonConvert.DeserializeObject<List<BikeStation>>(result).ToList();
                cache.AddToCache("dublin", stations);
            }

            var mystation = stations.SingleOrDefault(x => x.stationNumber == id);
            var nearbyStations = GPSHelper.FindNearbyStations(stations, mystation, metres);
            return nearbyStations;
        }

        [HttpGet]
        [Route("GetStation_NoAPIKey")]
        public object GetStation_XML_NoAPIKey(int stationId)         // N.B. returns XML, not JSON here
        {
            var result = dbhelper.GetStation_NoAPIRequired(stationId);
            var xmlSerializer = new XmlSerializer(typeof(BikeStationBasic));
            var stringreader = new StringReader(result);
            var xmlstation = (BikeStationBasic)xmlSerializer.Deserialize(stringreader);
            return xmlstation;
        }

        [HttpGet]
        [Route("XMLTest_GenerateSampleStation")]
        public object xmlTest_generateSampleStation()
        {
                    // making random bikestation model and returning it.
                    // some serialization problems; source API returns UTF-8 xml but this autogenerates UTF-16
                                //  also adds 2x    "xmlns" which are not needed
            BikeStationBasic bb= new BikeStationBasic();
            bb.available = 99;  bb.total = 88;
            bb.free = 77;   bb.updatedAt = 123456787654;
            bb.stationConnected = true; bb.stationOpen = true;

            var fff = new XmlSerializer(typeof(BikeStationBasic));
            var xml = "";
            var xmlsettings = new XmlWriterSettings();
            xmlsettings.Encoding = System.Text.Encoding.UTF8;

            using (var sww = new StringWriter())
            {
                using (XmlWriter wr = XmlWriter.Create(sww, xmlsettings))
                {
                    fff.Serialize(wr, bb);
                    xml = sww.ToString();
                }
            }
            return xml;
        }

    }
}
