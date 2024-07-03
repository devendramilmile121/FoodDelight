import { Component, Inject } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-create-update-menu-item',
  templateUrl: './create-update-menu-item.component.html',
  styleUrl: './create-update-menu-item.component.css',
})
export class CreateUpdateMenuItemComponent {
  title: string = 'Create';

  menuFg!: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<CreateUpdateMenuItemComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.title = this.data?.action === 'CREATE' ? 'Create' : 'Update';
    this.menuFg = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(250)]],
      description: ['', [Validators.required, Validators.maxLength(2000)]],
      price: ['', [Validators.required]],
      imagePath: ['', [Validators.required]],
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

  selectFile(event: any): void {
    this.menuFg.controls['imagePath'].markAsTouched();
    const file = event.target.files[0];
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      this.menuFg.controls['imagePath'].setValue(reader.result);
    };
  }

  onSubmit(): void {
    const value = this.menuFg.value;
    this.onClose({ action: 'refresh', data: value });
  }
}
