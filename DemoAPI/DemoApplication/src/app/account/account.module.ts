import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AccountRoutingModule } from './account-routing.module';
import { LoginComponent } from "./login.component";
import { DemoMaterialModule } from "../demo-material-module";
import { FlexLayoutModule } from '@angular/flex-layout';
@NgModule({
  declarations: [LoginComponent],
  imports: [
    CommonModule, DemoMaterialModule, FlexLayoutModule, FormsModule, ReactiveFormsModule,
    AccountRoutingModule
  ]
})
export class AccountModule { }
