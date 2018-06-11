import { Component } from '@angular/core';
import { EmployeeService } from '../../services/empservice.service';
@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {

    constructor(public nav: EmployeeService) { }

}
