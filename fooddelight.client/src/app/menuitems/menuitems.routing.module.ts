import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListMenuItemsComponent } from './list-menu-items/list-menu-items.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: ListMenuItemsComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MenuItemsRoutingModule { }
