import { NgModule } from '@angular/core';
import { EmployeeService } from './services/empservice.service'
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchEmployeeComponent } from './components/fetchemployee/fetchemployee.component';
import { createemployee } from './components/addemployee/AddEmployee.component';
import { htmlpage } from './components/htmlpage/htmlpage.component';
import { login } from './components/login/login.component';
import { assessment } from './components/assessment/assessment.component';
import { invoice } from './components/invoice/invoice.component';
import { reciepts } from './components/reciepts/reciepts.component';
import { addassessment } from './components/addassessment/addassessment.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        FetchEmployeeComponent,
        createemployee,
        htmlpage,
        login,
        assessment,
        addassessment,
        invoice,
        reciepts
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
            { path: 'menu/assessment', component: assessment },
            { path: 'menu/assessment/createassessment', component: addassessment },
            { path: 'menu/invoice', component: invoice },
            { path: 'menu/reciepts', component: reciepts },
            { path: 'login', component: login },
            { path: '**', redirectTo: 'login' },          
        ])
    ],
    providers: [EmployeeService]
})
export class AppModuleShared {
} 
