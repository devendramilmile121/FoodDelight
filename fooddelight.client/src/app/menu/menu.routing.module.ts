import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MenuListingComponent } from './menu-listing/menu-listing.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: MenuListingComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MenuRoutingModule { }
