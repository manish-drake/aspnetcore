import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class EmployeeService {
    myAppUrl: string = "";
    visible: boolean;

    constructor(private _http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.myAppUrl = baseUrl;
        this.visible = false;
    }
  
    getStateList() {
    
        return this._http.get(this.myAppUrl + 'api/Employee/GetStateList')
            .map(res => res.json())
            .catch(this.errorHandler);
    }  

    getEmployees() {
        return this._http.get(this.myAppUrl + 'api/Employee/Index')
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }  

    getEmployeeById(empId: number) {       
        return this._http.get(this.myAppUrl + "api/Employee/Details/" + empId)
            .map((response: Response) => response.json())
            .catch(this.errorHandler)
    }


    saveEmployee(employee) {
        return this._http.post(this.myAppUrl + 'api/Employee/Create', employee)
            .map((response: Response) => response.json())
            .catch(this.errorHandler)
    }  

    updateEmployee(employee) {
        debugger;
        return this._http.put(this.myAppUrl + 'api/Employee/Edit', employee)
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }  

    deleteEmployee(id) {
        return this._http.delete(this.myAppUrl + "api/Employee/Delete/" + id)
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    } 

    errorHandler(error: Response) {
        console.log(error);
        return Observable.throw(error);
    }   

    hide() { this.visible = false; }

    show() { this.visible = true; }

    toggle() { this.visible = !this.visible; }

    doSomethingElseUseful() { }

}