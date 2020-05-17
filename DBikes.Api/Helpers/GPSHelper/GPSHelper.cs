using DBikes.Api.Models.DBikesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBikes.Api.Helpers.GPSHelper
{
    public static class GPSHelper
    {
        public static List<BikeStation> FindNearbyStations(List<BikeStation> stations, BikeStation mystation, int metres)
        {
            var s =  stations.Where(x => FindDistance(mystation, x) < metres).ToList();
            return s;
        }

        public static int FindDistance(BikeStation mystation, BikeStation station)
        {
            /*
                Using GPS Lat/Lng
                ROUGH CALC, not exact! (doesnt need to be; just need ballpark nearby stations)
                Conversion: 1000m distance = approx 0.01 GPS       -> multiply x 10^6 
                - TODO: investigate GPS based on region/city to see if this GPS->Metres conversion varies
            */

            double latdiff = Math.Abs(mystation.position.lat - station.position.lat) * 100000;
            double lngdiff = Math.Abs(mystation.position.lng - station.position.lng) * 100000;  // to metres
            //  int simple_dist = (int)(latdiff > lngdiff ? latdiff : lngdiff);        // take larger
                                 // better to use Pythagoras! if lat == long, error would be 41%! (sqrt 2)
            double pythag_dist = Math.Sqrt(Math.Pow(latdiff, 2) + Math.Pow(lngdiff, 2));


            return (int) pythag_dist;
        }
    }
}