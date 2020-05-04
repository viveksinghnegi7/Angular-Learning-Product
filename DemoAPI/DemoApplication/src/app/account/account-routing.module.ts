import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from "./login.component";
import { RegisterComponent } from "./register/register.component";


const routes: Routes = [{
  path: '', component: LoginComponent,
  children: [
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
