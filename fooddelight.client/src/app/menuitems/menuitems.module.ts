import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListMenuItemsComponent } from './list-menu-items/list-menu-items.component';
import { CreateUpdateMenuItemComponent } from './create-update-menu-item/create-update-menu-item.component';
import { MenuItemsRoutingModule } from './menuitems.routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../common/material.module';
import { ConfirmationDialogComponent } from '../common/confirmation-dialog/confirmation-dialog.component';

@NgModule({
  declarations: [ListMenuItemsComponent, CreateUpdateMenuItemComponent],
  imports: [
    CommonModule,
    MenuItemsRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    ConfirmationDialogComponent,
  ],
})
export class MenuitemsModule {}
