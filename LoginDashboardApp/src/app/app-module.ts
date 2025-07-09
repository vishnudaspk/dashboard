import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
// FormsModule and NgChartsModule are now imported by standalone components directly
// CommonModule is implicitly available via BrowserModule or imported by standalone components

import { AppRoutingModule } from './app-routing-module'; // Corrected path: added hyphen
import { AppComponent } from './app';
// LoginComponent and DashboardComponent are now standalone, so not declared here.

import { AuthService } from './auth/auth';
import { DashboardService } from './dashboard/dashboard';
import { AuthGuard } from './auth/auth-guard';


@NgModule({
  declarations: [
    AppComponent // LoginComponent and DashboardComponent are standalone
  ],
  imports: [
    BrowserModule, // Exports CommonModule; AppComponent might need it if its template uses common directives
    AppRoutingModule, // Manages routes to standalone components
    HttpClientModule // For global HTTP services if any, or used by provided services
    // FormsModule and NgChartsModule are no longer needed here for Login/Dashboard components
  ],
  providers: [
    AuthService,
    DashboardService,
    AuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
