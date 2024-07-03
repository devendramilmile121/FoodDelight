import { Component, Inject, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { RESTAURANT_TYPES } from '../list-restaurant/list-restaurant.component';

@Component({
  selector: 'app-create-update-restaurant-dialog',
  templateUrl: './create-update-restaurant-dialog.component.html',
  styleUrl: './create-update-restaurant-dialog.component.css',
})
export class CreateUpdateRestaurantDialogComponent implements OnInit {
  title: string = 'Create';

  restaurantFg!: FormGroup;
  typeList: {value: number, name: string}[] = [
    {
      name: 'Pure Veg',
      value: 1
    },
    {
      name: 'Non Veg',
      value: 2
    },{
      name: 'Veg + Non Veg',
      value: 3
    }
  ];

  constructor(
    public dialogRef: MatDialogRef<CreateUpdateRestaurantDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.title = this.data?.action === 'CREATE' ? 'Create' : 'Update';
    this.restaurantFg = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(250)]],
      description: ['', [Validators.required, Validators.maxLength(2000)]],
      location: ['', [Validators.required, Validators.maxLength(250)]],
      type: ['', [Validators.required, Validators.required]],
      phone: ['', [Validators.required, Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]],
      email: ['', [Validators.required, Validators.email]],
    });

    if (this.data?.restaurant) {
      this.restaurantFg.patchValue(this.data?.restaurant);
    }
  }

  onClose(obj: { action: string; data: any }): void {
    this.dialogRef.close(obj);
  }

  get f(): { [key: string]: AbstractControl<any, any> } {
    return this.restaurantFg.controls;
  }

  onSubmit(): void {
    const value = this.restaurantFg.value;
    this.onClose({ action: 'refresh', data: value });
  }
}
