import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FullComponent } from './layouts/full/full.component';

const routes: Routes = [
    {
      path: '',
      component: FullComponent,
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
