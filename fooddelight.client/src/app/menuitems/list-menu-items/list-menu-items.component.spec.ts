import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListMenuItemsComponent } from './list-menu-items.component';

describe('ListMenuItemsComponent', () => {
  let component: ListMenuItemsComponent;
  let fixture: ComponentFixture<ListMenuItemsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ListMenuItemsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ListMenuItemsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
