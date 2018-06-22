import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../services/empservice.service';

@Component({
    templateUrl: './addofficetype.component.html'
})

export class addofficetype {
    constructor(public nav: EmployeeService) { }

    ngOnInit() {
        this.nav.show();
        this.nav.doSomethingElseUseful();
    }

    goBack() {
        window.history.go(-1);
    }
}