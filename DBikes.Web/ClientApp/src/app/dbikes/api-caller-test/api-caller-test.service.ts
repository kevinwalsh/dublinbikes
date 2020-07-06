import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-api-caller-test',
  templateUrl: './api-caller-test.component.html',
  styleUrls: ['./api-caller-test.component.css']
})
export class ApiCallerTestService {
    private http: HttpClient;
    private ApiUrl = 'http://localhost:51754/api';

    constructor(http: HttpClient) {
        this.http = http;
    }

    GetStationFromAPI(input:number): Observable<any> {
        return this.http.get(`${this.ApiUrl}/DublinBikes/GetStation/${input}`);
    }

  
}
