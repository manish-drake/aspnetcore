import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../services/empservice.service';

@Component({
    templateUrl: './forgotpassword.component.html'
})

export class forgotpassword implements OnInit {
    constructor(public nav: EmployeeService) { }

    ngOnInit() {
        this.nav.hide();
        this.nav.doSomethingElseUseful();
        //this.myform = new FormGroup({
        //    username: new FormControl('', [
        //        Validators.required
        //    ]),
        //    password: new FormControl('', [
        //        Validators.required
        //    ]),
        //});
    }   
}