import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../services/empservice.service';

@Component({
    templateUrl: './assessmenttype.component.html'
})

export class assessmenttype {
    constructor(public nav: EmployeeService) { }

    ngOnInit() {
        this.nav.show();
        this.nav.doSomethingElseUseful();
    }
}