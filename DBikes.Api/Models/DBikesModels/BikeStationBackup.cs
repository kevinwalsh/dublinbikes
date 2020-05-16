using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBikes.Api.Models.DBikesModels
{
    public class BikeStationBackup              // for use with XML
    {
        // will need to pass in StationID! not incl'd in result
        int stationNumber;
        string contractName;
        string stationName;
        double positionLat;
        double positionLong;

        int available;
        int free;
        int total;
        //bool stationOpen;
        //bool stationConnected;
        string status;
        bool isBanking;
        bool isBonus;
        DateTime updatedAt;


        public BikeStationBackup(int stnNum, string stnName, string contName, double latit, double longit,
            int av, int fr, int tot, string status, bool isBanking, bool isBonus, DateTime updated
            )
        {
            this.stationNumber = stnNum;
            this.stationName = stnName;
            this.contractName = contName;
            this.positionLat = latit;
            this.positionLong = longit;

            this.available = av;
            this.free = fr;
            this.total = tot;
            this.status = status;
            this.isBanking = isBanking;
            this.isBonus = isBonus;
            // updatedAt = MillisecToDatetime(updated);     // will I need to convert?
            this.updatedAt = updated;
        }

        
    }
}