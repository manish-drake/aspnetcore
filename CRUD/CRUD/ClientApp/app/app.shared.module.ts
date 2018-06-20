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
import { AdminNavMenuComponent } from './components/adminnavmenu/adminnavmenu.component';
import { assessmenttype } from './components/AssessmentType/assessmenttype.component';
import { insurancecompany } from './components/insurancecompany/insurancecompany.component';
import { bankaccount } from './components/BankAccount/bankaccount.component';
import { addassessmenttype } from './components/addassessmenttype/addassessmenttype.component';
import { addinsurancecompany } from './components/addinsurancecompany/addinsurancecompany.component';
import { addbankaccount } from './components/addbankaccount/addbankaccount.component';
import { licensetype } from './components/licensetype/licensetype.component';
import { addlicensetype } from './components/addlicensetype/addlicensetype.component';
import { model } from './components/model/model.component';
import { addmodel } from './components/addmodel/addmodel.component';
import { officetype } from './components/officetype/officetype.component';
import { addofficetype } from './components/addofficetype/addofficetype.component';
import { painttype } from './components/painttype/painttype.component';
import { addpainttype } from './components/addpainttype/addpainttype.component';
import { modelversion } from './components/modelversion/modelversion.component';
import { addmodelversion } from './components/addmodelversion/addmodelversion.component';
import { vehiclemake } from './components/vehiclemake/vehiclemake.component';
import { addvehiclemake } from './components/addvehiclemake/addvehiclemake.component';
import { workshop } from './components/workshop/workshop.component';
import { addworkshop } from './components/addworkshop/addworkshop.component';
import { addsurveyorfeeschedules } from './components/addsurveyorfeeschedules/addsurveyorfeeschedules.component';
import { surveyorfeeschedules } from './components/surveyorfeeschedules/surveyorfeeschedules.component';
import { addoffice } from './components/addoffice/addoffice.component';
import { office } from './components/office/office.component';



@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        AdminNavMenuComponent,
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
        addreceipt,
        assessmenttype,
        insurancecompany,
        bankaccount,
        addassessmenttype,
        addinsurancecompany,
        addbankaccount,
        licensetype,
        addlicensetype,
        model,
        addmodel,
        officetype,
        addofficetype,
        painttype,
        addpainttype,
        modelversion,
        addmodelversion,
        vehiclemake,
        addvehiclemake,
        workshop,
        addworkshop,
        surveyorfeeschedules,
        addsurveyorfeeschedules,
        addoffice,
        office
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
            { path: 'admin/assessmenttype', component: assessmenttype },
            { path: 'admin/assessmenttype/createassessmenttype', component: addassessmenttype },
            { path: 'admin/insurancecompany', component: insurancecompany },
            { path: 'admin/insurancecompany/createinsurancecompany', component: addinsurancecompany },
            { path: 'admin/bankaccount', component: bankaccount },
            { path: 'admin/bankaccount/createbankaccount', component: addbankaccount },
            { path: 'admin/licensetype', component: licensetype },
            { path: 'admin/licensetype/createlicensetype', component: addlicensetype },
            { path: 'admin/model', component: model },
            { path: 'admin/model/createmodel', component: addmodel },
            { path: 'admin/officetype', component: officetype },
            { path: 'admin/officetype/createofficetype', component: addofficetype },
            { path: 'admin/painttype', component: painttype },
            { path: 'admin/painttype/createpainttype', component: addpainttype },
            { path: 'admin/modelversion', component: modelversion },
            { path: 'admin/modelversion/createmodelversion', component: addmodelversion },
            { path: 'admin/vehiclemake', component: vehiclemake },
            { path: 'admin/vehiclemake/createvehiclemake', component: addvehiclemake },
            { path: 'admin/workshop', component: workshop },
            { path: 'admin/workshop/createworkshop', component: addworkshop },
            { path: 'admin/surveyorfeeschedules', component: surveyorfeeschedules },
            { path: 'admin/surveyorfeeschedules/createsurveyorfeeschedules', component: addsurveyorfeeschedules },
            { path: 'admin/office', component: office },
            { path: 'admin/office/createoffice', component: addoffice },
            { path: 'login', component: login },
            { path: '**', redirectTo: 'login' },          
        ])
    ],
    providers: [EmployeeService]
})
export class AppModuleShared {
} 
