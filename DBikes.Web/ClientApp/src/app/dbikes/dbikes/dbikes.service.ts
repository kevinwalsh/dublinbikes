import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { DBikesModel, CityEnum } from './dbikes.model';
import { AppSettingsService } from '../../helpers/app-settings/app-settings.service';

@Injectable()
export class DBikesService {
    baseurl;
    
    constructor(private http: HttpClient,
      private readonly appSettingsService: AppSettingsService
    ) {
      this.appSettingsService.$appSettingsObs.toPromise().then(x => {
        this.baseurl = x.apiUrl + '/api/DublinBikes';
      });
    }

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

    SearchAll(city: string, sortby: string, reverseOrder: boolean): Observable<DBikesModel[]> {
      let fullurl = this.baseurl + '/GetAllStations';
      if (city) { fullurl += '/'+ city; }
      if (sortby) { fullurl += '?sortby=' + sortby + '&reverseOrder=' + reverseOrder;}
      return this.http.get<DBikesModel[]>(fullurl, { headers: this.CreateAuthHeader() });
    }

    SearchSingle(stationNum: number, city: string): Observable<DBikesModel[]> {
        return this.http.get<DBikesModel[]>(this.baseurl + '/GetStation/'+ city + '/' + stationNum, { headers: this.CreateAuthHeader() });
    }

    SearchNearby(stationNum: number, city: string): Observable<DBikesModel[]> {
        return this.http.get<DBikesModel[]>(
            this.baseurl + '/GetStationsWithinMetres/' + city + '/' + stationNum + '/500'    //metres
            , { headers: this.CreateAuthHeader() }
        );
    }

}
