
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
        this.stationNum = x.number;
        this.city = x.contract_name;
        this.stationName = x.name;
        this.stationAddress = x.address;
        this.status = x.status;
        this.available_bike_stands = x.available_bike_stands;
        this.available_bikes = x.available_bikes;
        this.last_update = x.last_update;

        this.banking = x.banking;
        this.bonus = x.bonus;
    }

}
