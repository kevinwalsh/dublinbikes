import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { Subject } from 'rxjs';
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
        });
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
            });
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
            });
    }
}
