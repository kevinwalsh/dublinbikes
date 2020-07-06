
export class DBikesModel {
    public stationNum: number;
    public city: string;
    public stationName: string;
    stationAddress: string;
    status: string;
    position: string;       // lat & long
    bike_stands: number;
    available_bike_stands: number;
    available_bikes: number;
    //last_update: string;    //looks like millisec datetime?
    last_update:number

    //useless sounding info
    banking: boolean;
    bonus: boolean;

    constructor(x: any) {
        this.stationNum = x.stationNumber;
        this.city = x.contractName;
        this.stationName = x.stationName;
        this.stationAddress = x.stationName;
        this.status = x.status;
        this.available_bike_stands = x.freeStands;
        this.available_bikes = x.bikes;
        this.last_update = x.updatedAt;

        this.banking = x.isBanking;
        this.bonus = x.isBonus;

    }

}
