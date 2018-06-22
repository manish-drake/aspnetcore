import { Component } from '@angular/core';
import { EmployeeService } from '../../services/empservice.service';

@Component({
    selector: 'adminnav-menu',
    templateUrl: './adminnavmenu.component.html',
    styleUrls: ['./adminnavmenu.component.css']
})

export class AdminNavMenuComponent {
    constructor(public adminnav: EmployeeService) { }
}