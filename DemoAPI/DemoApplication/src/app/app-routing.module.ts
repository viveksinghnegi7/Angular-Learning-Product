import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FullComponent } from './layouts/full/full.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
    {
    path: '',
    component: LoginComponent,
      children: [
        {
          path: '',
          redirectTo: '',
          pathMatch: 'full'
        }//,
        //{
        //  path: '',
        //  loadChildren:
        //    () => import('./material-component/material.module').then(m => m.MaterialComponentsModule)
        //},
        //{
        //  path: 'dashboard',
        //  loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule)
        //}
      ]
    }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
