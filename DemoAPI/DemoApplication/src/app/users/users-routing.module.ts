import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UsersComponent } from "./user/users.component";
import { UserslistComponent } from './userslist/userslist.component';


const routes: Routes = [{ path: '', component: UserslistComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
