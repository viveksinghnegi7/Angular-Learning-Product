import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AccountRoutingModule } from './account-routing.module';
import { LoginComponent } from "./login.component";
import { DemoMaterialModule } from "../demo-material-module";@NgModule({
  declarations: [LoginComponent],
  imports: [
    CommonModule, DemoMaterialModule, FormsModule, ReactiveFormsModule,
    AccountRoutingModule
  ]
})
export class AccountModule { }
