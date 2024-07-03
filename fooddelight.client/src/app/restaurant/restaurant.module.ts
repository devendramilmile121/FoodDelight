import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateUpdateRestaurantDialogComponent } from './create-update-restaurant-dialog/create-update-restaurant-dialog.component';
import { ListRestaurantComponent } from './list-restaurant/list-restaurant.component';
import { RestaurantRoutingModule } from './restaurant.routing.module';
import { MaterialModule } from '../common/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ConfirmationDialogComponent } from '../common/confirmation-dialog/confirmation-dialog.component';

@NgModule({
  declarations: [
    CreateUpdateRestaurantDialogComponent,
    ListRestaurantComponent,
  ],
  imports: [
    CommonModule,
    RestaurantRoutingModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    ConfirmationDialogComponent,
  ],
})
export class RestaurantModule {}
