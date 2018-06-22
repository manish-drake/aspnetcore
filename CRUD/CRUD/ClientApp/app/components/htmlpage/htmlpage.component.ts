import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
import { EmployeeService } from '../../services/empservice.service';


@Component({
    templateUrl: './htmlpage.component.html'
})

export class htmlpage implements OnInit {
    constructor(public nav: EmployeeService) { }

    ngOnInit() {
        this.nav.show();
        this.nav.doSomethingElseUseful();
    }
    //public ngOnInit() {        
    //    $("#sidebar").mCustomScrollbar({
    //        theme: "minimal"
    //    });
    //}
    //sidebarCollapse() {        
    //    $('#sidebar, #content').toggleclass('active');
    //    $('.collapse.in').toggleclass('in');
    //    $('a[aria-expanded=true]').attr('aria-expanded', 'false');
    //}     
}



