import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { DBikesModel } from './dbikes.model';

@Injectable()
export class DBikesService {

 //   rawResults$: Observable<Array<any>>;
    baseurl = 'https://localhost:44303/api/DublinBikes';
    // baseurl = 'http://dublinbikesapi.azurewebsites.net/api/DublinBikes';
    
    /*  sample single
            https://api.jcdecaux.com/vls/v1/stations/{station_number}?contract={contract_name} 
        sample all in area
            https://api.jcdecaux.com/vls/v1/stations?contract={contract_name}&apiKey={api_key}
    */

    constructor(private http: HttpClient) { }

    private GenerateAuthToken(): string {
        let key = "dublinbikestoken"; 
//        let d = new Date().toISOString().substring(0, 10);      // yyyy-MM-dd
        let d = Math.floor(new Date().getTime() /Math.pow(10,6));      //round unix time to approx 15mins
        let chararray = key + d;
        let hash = 0;
        for (var i = 0; i < chararray.length; i++) {
            let ch = chararray.charCodeAt(i);        
            hash = ((hash << 5) - hash) + ch;       // equiv to hash * 31 + char, but faster
            hash |= 0;
        }
        return hash.toString();
    }

    private CreateAuthHeader(): HttpHeaders {
        let authHeader = new HttpHeaders({ 'Authorization': 'Bearer ' + this.GenerateAuthToken() });
        return authHeader;      // HttpHeaders immutable; must explicitly create with desired headers instead of adding later
    }

    SearchAll(): Observable<DBikesModel[]> {
        return this.http.get<DBikesModel[]>(this.baseurl + '/GetAllStations', { headers: this.CreateAuthHeader()});
    }

    SearchSingle(stationNum: number): Observable<DBikesModel[]> {
        return this.http.get<DBikesModel[]>(this.baseurl + '/GetStation/' + stationNum, { headers: this.CreateAuthHeader() });
    }

    SearchNearby(stationNum: number): Observable<DBikesModel[]> {
        return this.http.get<DBikesModel[]>(
            this.baseurl + '/GetStationsWithinMetres/' + stationNum + '/500'    //metres
            , { headers: this.CreateAuthHeader() }
        );
    }

}