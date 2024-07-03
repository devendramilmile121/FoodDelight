import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MenuListingComponent } from './menu-listing.component';

describe('MenuListingComponent', () => {
  let component: MenuListingComponent;
  let fixture: ComponentFixture<MenuListingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MenuListingComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MenuListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
