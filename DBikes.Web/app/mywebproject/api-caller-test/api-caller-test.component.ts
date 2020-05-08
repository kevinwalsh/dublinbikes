import { Component, OnInit } from '@angular/core';
import { ApiCallerTestService } from './api-caller-test.service';


@Component({
  selector: 'app-api-caller-test',
  templateUrl: './api-caller-test.component.html',
  styleUrls: ['./api-caller-test.component.css']
})
export class ApiCallerTestComponent implements OnInit {

    mynumber: number;

    constructor(
        public apiCallerTestService: ApiCallerTestService
    ) { }

    ngOnInit() {
        this.apiCallerTestService.GetStationFromAPI(3)
          .subscribe(x => {
              this.mynumber = x;
          });
    }

    PostNewNumber(n: number) {
        this.apiCallerTestService.GetStationFromAPI(n)
            .subscribe(x => {
                this.mynumber = x;
            });

    }
  
}
