import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../services/empservice.service';
import { Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { AssessmentTypeService } from '../../Services/assessmenttype.service';



@Component({
    templateUrl: './assessmenttype.component.html'
})

export class assessmenttype {
    public assessmentList: AssessmentTypeData[] = [];

    constructor(public http: Http, private _router: Router, private _assessmenttypeService: AssessmentTypeService, public nav: EmployeeService) {
        this.getAssessmentType();
    }

    getAssessmentType() {
        this._assessmenttypeService.getAssessmentType().subscribe(
            data => this.assessmentList = data
        )
    }
    
    ngOnInit() {
        this.nav.show();
        this.nav.doSomethingElseUseful();
    }
}

interface AssessmentTypeData {
    assessmentTypeId: number;
    assessmentType: string;
   
}