import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateUpdateMenuItemComponent } from './create-update-menu-item.component';

describe('CreateUpdateMenuItemComponent', () => {
  let component: CreateUpdateMenuItemComponent;
  let fixture: ComponentFixture<CreateUpdateMenuItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateUpdateMenuItemComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateUpdateMenuItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
