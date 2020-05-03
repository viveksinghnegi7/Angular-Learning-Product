import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FullComponent } from './layouts/full/full.component';
import { LoginComponent } from './account/login.component';
import { AuthGuard } from "./helpers/auth.gaurd";

const accountModule = () => import('./account/account.module').then(x => x.AccountModule);
const routes: Routes = [
    {
    path: '',
    component: FullComponent, canActivate: [AuthGuard],
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
  },
    { path: 'account', loadChildren: accountModule },
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
