import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { promise } from 'protractor';
import { shareReplay } from 'rxjs/operators';

@Injectable()
export class AppSettingsService {
  appSettings;
  $appSettingsObs;
  
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {        // copying template from "fetch-data", calling weathercontroller
    this.$appSettingsObs = http.get(baseUrl + 'settings').pipe(shareReplay(1));
  }

  public GetAppSettings(): any {
    if (!this.appSettings) {
      this.$appSettingsObs.toPromise().then(x => {
        this.appSettings = x; return x;
      })
    }
    else return this.appSettings;
  }
}
