import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../services/empservice.service';
import { Router } from '@angular/router';

@Component({
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})

export class login implements OnInit {
    constructor(public nav: EmployeeService , private router: Router) { }

    ngOnInit() {
        this.nav.hide();
        this.nav.doSomethingElseUseful();
    }

    validation() {
        this.router.navigate(['menu']);
    }
}

