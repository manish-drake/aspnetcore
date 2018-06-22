import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../services/empservice.service';

@Component({
    templateUrl: './addoffice.component.html'
})

export class addoffice {
    constructor(public nav: EmployeeService) { }

    ngOnInit() {
        this.nav.show();
        this.nav.doSomethingElseUseful();
    }

    goBack() {
        window.history.go(-1);
    }
}