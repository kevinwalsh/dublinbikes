import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';

import { TemplateComponent } from './dbikes/template/template.component';
import { DashboardComponent } from './dbikes/Dashboard/dashboard.component';
import { DBikesDashboardComponent } from './dbikes/dbikes/dbikes-dashboard.component';
import { DBikesService } from './dbikes/dbikes/dbikes.service';
import { LogicAppService } from './helpers/logicappservice/logicapp.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    DashboardComponent,
    TemplateComponent,
    DBikesDashboardComponent,
   ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: DashboardComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'dbikes/dashboard', component: DashboardComponent },
      { path: 'dbikes/template', component: TemplateComponent }
   ])
  ],
  providers: [
    DBikesService,
    LogicAppService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
