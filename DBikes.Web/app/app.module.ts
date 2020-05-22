import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent }  from './app.component';
import { DashboardComponent } from './mywebproject/Dashboard/Dashboard.component';
import { TemplateComponent } from './mywebproject/template/template.component';
import { AppRoutingModule } from './app-routing.module';
import { DBikesDashboardComponent } from './mywebproject/dbikes/dbikes-dashboard.component';
import { DBikesService} from './mywebproject/dbikes/dbikes.service';


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
        DBikesDashboardComponent,
        AppComponent
    ],
    providers: [
        DBikesService
        ],
  bootstrap:    [ AppComponent ]
})
export class AppModule { }