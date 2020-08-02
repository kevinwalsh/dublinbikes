using DBikes.Api.Helpers.GPSHelper;
using DBikes.Api.Helpers.HTTPClient;
using DBikes.Api.Models.DBikesModels;
using DBikes.Api.Providers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
using System.Xml.Serialization;
using System.Threading.Tasks;
using DBikes.Api.Filters;
using DBikes.CoreApi.SettingsOptions;
using DBikes.Api.Models;

namespace DBikes.Api.Controllers
{
    [SimpleAuthentication]
    //[HMACAuthentication]            //https://bitoftech.net/2014/12/15/secure-asp-net-web-api-using-api-key-authentication-hmac-authentication/
    [Route("api/DublinBikes")]
    public class DublinBikesController : Controller
    {
        private DBikesMemoryCache cache;
        private MySettingsOptions mySettingsOptions;
        private DublinBikesHTTPClientHelper dbhelper;

        public DublinBikesController(DBikesMemoryCache memcache, 
            MySettingsOptions mySettings,
            DublinBikesHTTPClientHelper dbikesHTTP
            )
        {
            cache = memcache;
            mySettingsOptions = mySettings;
            dbhelper = dbikesHTTP;
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
        [Route("GetStation/{city}/{id}")]
        public async Task<IActionResult> GetStationById(CityEnum city, int id=11)
        {
             List<BikeStation> stations = (List<BikeStation>) cache.CheckCache(city.ToString());
            BikeStation station = null;
            if (stations != null)
            {
                station = stations.Single(x => x.stationNumber == id);
            }
            else
            {
                string result = await dbhelper.GetStation(city.ToString(), id);
                station = JsonConvert.DeserializeObject<BikeStation>(result);
            }
            List<BikeStation> stationAsList = new List<BikeStation>() { station };
            return Ok(stationAsList);
        }

        /// <summary>
        /// Debug function to return model with properties exactly matching parameters of expected JSON.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetStationExactModel/{id}")]
        public async Task<IActionResult> GetStationExactModel(int id=11)
        {
            string result = await dbhelper.GetStation(mySettingsOptions.DefaultCity, id);
            BikeStationExact bse = JsonConvert.DeserializeObject<BikeStationExact>(result);
            return Ok(bse);
        }

        [HttpGet]
        [Route("GetAllStations")]
        public async Task<IActionResult> GetAllStations()
        {
            List<BikeStation> stations = (List<BikeStation>) cache.CheckCache(mySettingsOptions.DefaultCity);
            if (stations == null)
            {
                string result = await dbhelper.GetAllStations();
                stations = JsonConvert.DeserializeObject<List<BikeStation>>(result).ToList();
                cache.AddToCache(mySettingsOptions.DefaultCity, stations);
            }
            
            //            return stations;
            return Ok(stations.OrderBy(x=>x.stationNumber));
        }

        [HttpGet]
        [Route("GetAllStations/{city}")]
        public async Task<IActionResult> GetAllStations(Models.CityEnum city, string sortby, bool reverseOrder)
        {
            List<BikeStation> stations = (List<BikeStation>)cache.CheckCache(city.ToString());
            if (stations == null)
            {
                string result = await dbhelper.GetAllStations(city.ToString());
                stations = JsonConvert.DeserializeObject<List<BikeStation>>(result).ToList();
                cache.AddToCache(city.ToString(), stations);
            }
            //ordering
            switch (sortby)
            {       // ['stationid', 'stationname', 'bikes', 'spaces', 'updated','latlong'];
                case "stationname":
                    stations = stations.OrderBy(x => x.stationName).ToList();
                    break;
                case "bikes":
                    stations = stations.OrderBy(x => x.bikes).ToList();
                    break;
                case "spaces":
                    stations = stations.OrderBy(x => x.freeStands).ToList();
                    break;
                case "updated":
                    stations = stations.OrderByDescending(x => x.updatedAt).ToList();       //reverse lat (north hemisphere)
                    break;
                case "latlong":
                    stations = stations.OrderByDescending(x => x.position.lat).ThenBy(x=>x.position.lng).ToList();  // N-S
                    break;
                case "stationid":
                default:
                    stations = stations.OrderBy(x => x.stationNumber).ToList();
                    break;
            }
            if(reverseOrder) { stations.Reverse(); }

            //            return stations;
            return Ok(stations);
        }

        [HttpGet]
        [Route("GetStationsWithinMetres/{city}/{id}/{metres}")]
        public async Task<IActionResult> GetStationsWithinMetres(CityEnum city, int id, int metres = 0)
        {
            List<BikeStation> stations = (List<BikeStation>)cache.CheckCache(mySettingsOptions.DefaultCity);
            if (stations == null)
            {
                string result = await dbhelper.GetAllStations(city.ToString());
                stations = JsonConvert.DeserializeObject<List<BikeStation>>(result).ToList();
                cache.AddToCache(city.ToString(), stations);
            }
            metres = metres > 0 ? metres : mySettingsOptions.DefaultSearchRadius; 

            var mystation = stations.SingleOrDefault(x => x.stationNumber == id);
            var nearbyStations = GPSHelper.FindNearbyStations(stations, mystation, metres);
            return Ok(nearbyStations);
        }

        [HttpGet]
        [Route("GetStation_NoAPIKey")]
        public async Task<IActionResult> GetStation_XML_NoAPIKey(int stationId)         // N.B. returns XML, not JSON here
        {
            var result = await dbhelper.GetStation_NoAPIRequired(stationId);
            var xmlSerializer = new XmlSerializer(typeof(BikeStationBasic));
            var stringreader = new StringReader(result);
            var xmlstation = (BikeStationBasic)xmlSerializer.Deserialize(stringreader);
            return Ok(xmlstation);
        }

        [HttpGet]
        [Route("XMLTest_GenerateSampleStation")]
        public async Task<IActionResult> xmlTest_generateSampleStation()
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
            return Ok(xml);
        }

    }
}
