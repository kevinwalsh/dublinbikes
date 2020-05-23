import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { DBikesService } from './dbikes.service';
import { DBikesModel } from './dbikes.model';

@Component({
    selector: 'app-dbikes-dashboard-component',
    templateUrl: './dbikes-dashboard.component.html',
    styleUrls: ['./dbikes-dashboard.component.css']
})
export class DBikesDashboardComponent implements OnInit {
    obs: Observable<any>;
    results: DBikesModel[];
    lowBikeLimit: number = 3;

    selectedStation: number = 2;
    lastupdate: number;

    constructor(
        private bikeService: DBikesService
    ) { }

    ngOnInit() {
        this.obs = this.bikeService.SearchAll();
    }

    SearchAllStations() {
        this.obs.subscribe(x => {
            let r = [];
            for (let el in x) {
                r.push(new DBikesModel(x[el]));
            }
            this.results = r;
            this.lastupdate = Date.now();
        },
            error => {
                this.HandleHTTPError(error);
            }
        );
    }

    SearchSingleStation() {
        let station = this.selectedStation;         // model bound to html
        this.bikeService.SearchSingle(station)
            .subscribe(x => {
                let b = new DBikesModel(x);
                this.results = [];
                this.results.push(b);
                let c = x as DBikesModel;

                this.lastupdate = Date.now();
            },
            error => {
                this.HandleHTTPError(error);
            }
        );
    }

    WatchStation() {
        alert('TODO: set auto-update');
    }

    ShowNearbyStations(station:number) {
        //let station = 15;
        this.bikeService.SearchNearby(station)
            .subscribe(x => {
                let r = [];
                for (let el in x) {
                    r.push(new DBikesModel(x[el]));
                }
                this.results = r;

                this.lastupdate = Date.now();
            },
            error => {
                this.HandleHTTPError(error);
            }
        );
    }

    HandleHTTPError(error: any) {
        switch (error.status) {
            case 404:
                alert("404 Error: station not found:");
                break;
            case 403:
                alert("403 Error: The server rejected the request");
                console.error('403 Error (this may be due to an invalid API Key on the intermediate server).'
                    + '\n ' + JSON.stringify(error));
                break;
            case 500:
                alert("500 Error: Internal server error");
                console.error('500 Error: \n ' + JSON.stringify(error));
                break;
            default:
                alert("Error communicating with server");
                break;
        }
        this.results = [];
    }
}
