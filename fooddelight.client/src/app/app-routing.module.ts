import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'pages/restaurant',
    loadChildren: () =>
      import('./restaurant/restaurant.module').then((m) => m.RestaurantModule),
  },
  {
    path: 'pages/menu/:id',
    loadChildren: () => import('./menu/menu.module').then((m) => m.MenuModule),
  },
  {
    path: 'pages/menu-items/:id',
    loadChildren: () =>
      import('./menuitems/menuitems.module').then((m) => m.MenuitemsModule),
  },
  { path: '', redirectTo: 'pages/restaurant', pathMatch: 'full' },
  { path: '**', redirectTo: 'pages/restaurant' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
