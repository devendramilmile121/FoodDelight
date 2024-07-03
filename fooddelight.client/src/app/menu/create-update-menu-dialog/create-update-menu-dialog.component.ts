import { Component, Inject } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-create-update-menu-dialog',
  templateUrl: './create-update-menu-dialog.component.html',
  styleUrl: './create-update-menu-dialog.component.css'
})
export class CreateUpdateMenuDialogComponent {
  title: string = 'Create';

  menuFg!: FormGroup;
  typeList: {value: number, name: string}[] = [
    {
      name: 'Veg',
      value: 1
    },
    {
      name: 'Non Veg',
      value: 2
    }
  ];

  constructor(
    public dialogRef: MatDialogRef<CreateUpdateMenuDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.title = this.data?.action === 'CREATE' ? 'Create' : 'Update';
    this.menuFg = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(250)]],
      menuType: ['', [Validators.required]]
    });

    if (this.data?.restaurant) {
      this.menuFg.patchValue(this.data?.restaurant);
    }
  }

  onClose(obj: { action: string; data: any }): void {
    this.dialogRef.close(obj);
  }

  get f(): { [key: string]: AbstractControl<any, any> } {
    return this.menuFg.controls;
  }

  onSubmit(): void {
    const value = this.menuFg.value;
    this.onClose({ action: 'refresh', data: value });
  }
}
