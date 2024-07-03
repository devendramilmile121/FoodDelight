import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateUpdateMenuDialogComponent } from './create-update-menu-dialog.component';

describe('CreateUpdateMenuDialogComponent', () => {
  let component: CreateUpdateMenuDialogComponent;
  let fixture: ComponentFixture<CreateUpdateMenuDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateUpdateMenuDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateUpdateMenuDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
