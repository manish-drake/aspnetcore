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
import { receipts } from './components/receipts/receipts.component';
import { addassessment } from './components/addassessment/addassessment.component';
import { addinvoice } from './components/addinvoice/addinvoice.component';
import { addreceipt } from './components/addreceipt/addreceipt.component';

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
        addinvoice,
        receipts,
        addreceipt
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
            { path: 'menu/invoice/createinvoice', component: addinvoice },
            { path: 'menu/receipts', component: receipts },
            { path: 'menu/receipts/createreceipt', component: addreceipt },
            { path: 'login', component: login },
            { path: '**', redirectTo: 'login' },          
        ])
    ],
    providers: [EmployeeService]
})
export class AppModuleShared {
} 
