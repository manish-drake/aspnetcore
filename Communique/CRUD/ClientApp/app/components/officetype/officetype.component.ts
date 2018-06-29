import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../services/empservice.service';
import { Http } from '@angular/http';
import { Router } from '@angular/router';
import { OfficeTypeService } from '../../Services/officetype.service';

@Component({
    templateUrl: './officetype.component.html'
})

export class officetype {
    public officetypeList: OfficeTypeData[] = [];

    constructor(public http: Http, private _router: Router, private _officetypeService: OfficeTypeService,public nav: EmployeeService) { }

    ngOnInit() {
        this.nav.show();
        this.nav.doSomethingElseUseful();
    }

    getOfficeType() {
        this._officetypeService.getOfficeType().subscribe(
            data => this.officetypeList = data
        )
    }
}
interface OfficeTypeData {
    officeTypeId: number;
    officeType: string;
}