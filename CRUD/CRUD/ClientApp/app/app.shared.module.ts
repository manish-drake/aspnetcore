import { NgModule } from '@angular/core';
import { EmployeeService } from './services/empservice.service'
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './components/app/app.component';
//import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchEmployeeComponent } from './components/fetchemployee/fetchemployee.component';
import { createemployee } from './components/addemployee/AddEmployee.component';
import { htmlpage } from './components/htmlpage/htmlpage.component';
import { login } from './components/login/login.component';


@NgModule({
    declarations: [
        AppComponent,
      //  NavMenuComponent,
        HomeComponent,
        FetchEmployeeComponent,
        createemployee,
        htmlpage,
        login,
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'login', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'fetch-employee', component: FetchEmployeeComponent },
            { path: 'register-employee', component: createemployee },
            { path: 'employee/edit/:id', component: createemployee },
            { path: 'menu', component: htmlpage },
            { path: 'login', component: login },
            { path: '**', redirectTo: 'login' }
        ])
    ],
    providers: [EmployeeService]
})
export class AppModuleShared {
} 
