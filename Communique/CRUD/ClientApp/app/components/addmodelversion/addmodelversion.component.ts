﻿import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../services/empservice.service';

@Component({
    templateUrl: './addmodelversion.component.html'
})

export class addmodelversion {
    constructor(public nav: EmployeeService) { }

    ngOnInit() {
        this.nav.show();
        this.nav.doSomethingElseUseful();
    }

    goBack() {
        window.history.go(-1);
    }
}