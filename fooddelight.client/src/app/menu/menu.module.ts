import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateUpdateMenuDialogComponent } from './create-update-menu-dialog/create-update-menu-dialog.component';
import { MenuListingComponent } from './menu-listing/menu-listing.component';
import { ConfirmationDialogComponent } from '../common/confirmation-dialog/confirmation-dialog.component';
import { MaterialModule } from '../common/material.module';
import { MenuRoutingModule } from './menu.routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [CreateUpdateMenuDialogComponent, MenuListingComponent],
  imports: [
    CommonModule,
    MenuRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    ConfirmationDialogComponent,
  ],
})
export class MenuModule {}
