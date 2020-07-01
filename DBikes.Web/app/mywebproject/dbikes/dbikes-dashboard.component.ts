import { Component, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { DBikesService } from './dbikes.service';
import { DBikesModel } from './dbikes.model';
import { timer } from 'rxjs/observable/timer';

@Component({
    selector: 'app-dbikes-dashboard-component',
    templateUrl: './dbikes-dashboard.component.html',
    styleUrls: ['./dbikes-dashboard.component.css']
})
export class DBikesDashboardComponent implements OnInit {
    loading: boolean = true;
    polltime: number = 15000;
    pagetimeouttime: number = 1000 * 60 * 30;       // 30mins is generous
    results: DBikesModel[];
    lowBikeLimit: number = 3;
    selectedStation: number = 2;
    lastupdate: number;
    liveUpdateBoolean: boolean = false;

    DBikesObs$: Observable<DBikesModel[]>;         // will swap out method calls as required
    DBikesSubscription$: Subscription;
    public timerObs$: Observable<any>;
    public timerSub$: Subscription;

    constructor(
        private bikeService: DBikesService
    ) { }

    ngOnInit() {
        this.timerObs$ = timer(0, this.polltime);
        this.DBikesObs$ = this.bikeService.SearchAll();
        this.timerSub$ = this.timerObs$.subscribe(t => {
            if (this.liveUpdateBoolean) {
                this.DBikesSubscription$ = this.DBikesObs$.subscribe(x => {
                    this.ParseResults(x);
                    this.CheckSubscriptionUnsubscribe(t);       // auto-timeout clients to reduce server load
                },
                    error => this.HandleHTTPError(error)
                );
            }
            else {
                // do nothing
            }
        });
        this.loading = false;
    }
   
    ParseResults(x: DBikesModel[]) {
        this.loading = false;
        let r = [];
        for (let el in x) {
            r.push(new DBikesModel(x[el]));
        }
        this.results = r;
        this.lastupdate = Date.now();
    }
    
    ToggleLiveUpdate() {
        this.liveUpdateBoolean = this.liveUpdateBoolean == true ? false : true;
    }

    CheckSubscriptionUnsubscribe(x: any) {
                    // disable autoupdate after set time, to counteract client pages left open impacting server
        if (x >= (this.pagetimeouttime / this.polltime)) {
            this.timerSub$.unsubscribe();
            alert('Page timeout: auto-updates disabled after ' +
                (this.pagetimeouttime / 1000) + ' seconds. Please refresh page to continue');
            document.getElementById('SearchAllBtn').setAttribute('disabled', 'true');
            document.getElementById('SearchSingleBtn').setAttribute('disabled', 'true');
            document.getElementById('ToggleLiveUpdateBtn').setAttribute('disabled', 'true');
            document.getElementById('results').remove();
        }
    }

    SearchAllStations() {
        this.loading = true;
        this.DBikesObs$ = this.bikeService.SearchAll();

        this.DBikesObs$.toPromise().then(x => {     // fire once on click, while waiting for timerObs$
                this.ParseResults(x);
            },
            error => this.HandleHTTPError(error)
        );
    }


    SearchSingleStation() {
        this.loading = true;
        this.DBikesObs$ = this.bikeService.SearchSingle(this.selectedStation);

        this.DBikesObs$.toPromise().then(x => {    // fire once on click, while waiting for timerObs$
            this.ParseResults(x);
        },
            error => this.HandleHTTPError(error)
        );

    }

    ShowNearbyStations(stationNum: number) {
        this.loading = true;
        this.DBikesObs$ = this.bikeService.SearchNearby(stationNum);

        this.DBikesObs$.toPromise().then(x => {    // fire once on click, while waiting for timerObs$
            this.ParseResults(x);
        },
            error => this.HandleHTTPError(error)
        );
    }

    HandleHTTPError(error: any) {
        this.loading = false;
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
