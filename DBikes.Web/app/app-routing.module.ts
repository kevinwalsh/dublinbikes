import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import {DashboardComponent } from './mywebproject/Dashboard/Dashboard.component';
import { TemplateComponent } from './mywebproject/template/template.component';

const routes: Routes = [
  { path: '', redirectTo: '/mywebproject/dashboard', pathMatch: 'full' },
  { path: 'mywebproject/dashboard', component: DashboardComponent},
  { path: 'mywebproject/template', component: TemplateComponent }

];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
