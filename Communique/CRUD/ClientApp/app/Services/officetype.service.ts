import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class OfficeTypeService {
    myAppUrl: string = "";
    visible: boolean;

    constructor(private _http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.myAppUrl = baseUrl;
        this.visible = false;
    }   

    getOfficeType() {
        return this._http.get(this.myAppUrl + 'api/OfficeType/Index')
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }
    
    errorHandler(error: Response) {
        console.log(error);
        return Observable.throw(error);
    }
}