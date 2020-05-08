import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent }  from './app.component';
import { DashboardComponent } from './mywebproject/Dashboard/Dashboard.component';
import { TemplateComponent } from './mywebproject/template/template.component';
import { AppRoutingModule } from './app-routing.module';
import { ApiCallerTestService } from './mywebproject/api-caller-test/api-caller-test.service';
import { ApiCallerTestComponent } from './mywebproject/api-caller-test/api-caller-test.component';

@NgModule({
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule
    ],
    declarations: [
        DashboardComponent,
        TemplateComponent,
        ApiCallerTestComponent,
        AppComponent
    ],
    providers: [
        ApiCallerTestService
        ],
  bootstrap:    [ AppComponent ]
})
export class AppModule { }