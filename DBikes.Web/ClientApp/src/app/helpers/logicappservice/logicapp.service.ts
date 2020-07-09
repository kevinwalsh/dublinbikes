import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class LogicAppService {

    constructor(private http: HttpClient) { }

    logicapp_url = "placeholder_url";

    SendErrorToStorageAccount(errType: string, errMsg: string, srcFunction: string) : Observable<any>{
      let data = {
        "dberror": {
          "PartitionKey": this.MakeGUID(),
          "RowKey": this.MakeGUID(),
          "errormessage": errMsg,
          "errortype": errType,
          "sourcefunction": srcFunction
        }
      };
      return this.http.post<any>(this.logicapp_url,
        data,
        { headers: { "Content-Type": "application/json" } }
      );
  }

      // GUID GENERATOR
      // from https://stackoverflow.com/questions/26501688/a-typescript-guid-class
      // general guid structure is below:         the 0-1 digits below should be hexadecimal   =>  [0-9,a-f],
      //      "4" should have value [1 - 5],                "8" =>    ["8", "9", "a", or "b"]
      // Many sources say Math.Random() is too low-quality for this
      // instead, found a "crypto" in-built JS library
      // also, this ".replace(/018/ is new to me but it works nicely

  MakeGUID(): string {
    let g = '10000000-1000-4000-8000-100000000000'.replace(/[018]/g, c =>
      (Number.parseInt(c) ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> Number.parseInt(c) / 4).toString(16)
    );
    return g;
  }

}
