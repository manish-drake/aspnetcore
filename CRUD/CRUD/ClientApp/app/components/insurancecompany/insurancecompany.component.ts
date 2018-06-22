import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../services/empservice.service';

@Component({
    templateUrl: './insurancecompany.component.html'
})

export class insurancecompany {
    constructor(public nav: EmployeeService) { }

    ngOnInit(){
        this.nav.show();
        this.nav.doSomethingElseUseful();
    }
}