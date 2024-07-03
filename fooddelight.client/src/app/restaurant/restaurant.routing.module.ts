import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListRestaurantComponent } from './list-restaurant/list-restaurant.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: ListRestaurantComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RestaurantRoutingModule { }
