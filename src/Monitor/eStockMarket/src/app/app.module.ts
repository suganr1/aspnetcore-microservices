import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { DecimalPipe } from "@angular/common";

import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppMaterialModule } from './shared/app.material.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { HttpClientModule } from '@angular/common/http';

import { EnvServiceProvider } from "./_services/env.service.provider";
import { CommonService } from './_services/common.service';
import { CompanyService } from './_services/company.service';

import { AppComponent } from './app.component';
import { LayoutComponent } from "./shared/layout/layout.component";

import { HomeComponent } from './home/home.component';
import { CompanyComponent } from './company/company.component';
import { StockComponent } from './stock/stock.component';
import { CreateCompanyComponent } from './company/create-company/create-company.component';
import { AddStockComponent } from './stock/add-stock/add-stock.component';
import { ConfirmDialogComponent } from "./shared/confirm-dialog/confirm-dialog.component";

import { UpperCaseInputDirective } from './shared/directive/upper-case-input.directive';

@NgModule({
  entryComponents: [
    ConfirmDialogComponent
  ],
  declarations: [
    AppComponent,
    LayoutComponent,
    HomeComponent,
    CompanyComponent,
    StockComponent,
    CreateCompanyComponent,
    ConfirmDialogComponent,
    AddStockComponent,
    UpperCaseInputDirective
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    AppMaterialModule,
    FlexLayoutModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [DecimalPipe, EnvServiceProvider, CompanyService, CommonService],
  bootstrap: [AppComponent]
})
export class AppModule { }
