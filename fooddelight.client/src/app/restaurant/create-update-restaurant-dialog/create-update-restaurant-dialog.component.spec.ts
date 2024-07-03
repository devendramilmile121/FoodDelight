import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateUpdateRestaurantDialogComponent } from './create-update-restaurant-dialog.component';

describe('CreateUpdateRestaurantDialogComponent', () => {
  let component: CreateUpdateRestaurantDialogComponent;
  let fixture: ComponentFixture<CreateUpdateRestaurantDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateUpdateRestaurantDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateUpdateRestaurantDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
