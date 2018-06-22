import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../services/empservice.service';

@Component({
    templateUrl: './officetype.component.html'
})

export class officetype {
    constructor(public nav: EmployeeService) { }

    ngOnInit() {
        this.nav.show();
        this.nav.doSomethingElseUseful();
    }
}