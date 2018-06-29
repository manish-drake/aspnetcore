import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../services/empservice.service';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})

export class login implements OnInit {
    constructor(public nav: EmployeeService, private router: Router) { }
    myform: FormGroup | undefined;

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

    validation() {
        this.router.navigate(['menu']);
    }
}

