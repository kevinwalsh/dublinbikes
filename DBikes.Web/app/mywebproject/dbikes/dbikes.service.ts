import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/switchMap';
import { DBikesModel } from './dbikes.model';

@Injectable()
export class DBikesService {

 //   rawResults$: Observable<Array<any>>;
    baseurl = 'http://localhost:51754/api/DublinBikes/';
    
    /*  sample single
            https://api.jcdecaux.com/vls/v1/stations/{station_number}?contract={contract_name} 
        sample all in area
            https://api.jcdecaux.com/vls/v1/stations?contract={contract_name}&apiKey={api_key}
    */

    constructor(private http: HttpClient) { }

    SearchAll(): Observable<DBikesModel[]> {
        return this.http.get<DBikesModel[]>(this.baseurl + '/GetAllStations');
    }

    SearchSingle(stationNum: number): Observable<DBikesModel> {
        return this.http.get<DBikesModel>(this.baseurl + '/GetStation/' + stationNum);
    }

    SearchNearby(stationNum: number): Observable<DBikesModel[]> {
        return this.http.get<DBikesModel[]>(
            this.baseurl + '/GetStationsWithinMetres/' + stationNum + '/500'    //metres
        );
    }

}