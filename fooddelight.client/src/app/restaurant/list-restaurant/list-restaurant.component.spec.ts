import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of, Subject, throwError } from 'rxjs';
import {
  ListRestaurantComponent,
  RESTAURANT_TYPES,
} from './list-restaurant.component';
import { RestaurantService } from '../../services/restraurant.service';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MaterialModule } from '../../common/material.module';
import { ConfirmationDialogComponent } from '../../common/confirmation-dialog/confirmation-dialog.component';

describe('ListRestaurantComponent', () => {
  let component: ListRestaurantComponent;
  let fixture: ComponentFixture<ListRestaurantComponent>;
  let mockRestaurantService: any;
  let mockMatDialog: any;
  let mockRouter: any;
  let mockMatSnackBar: any;

  beforeEach(async () => {
    mockRestaurantService = {
      getAll: jasmine.createSpy('getAll').and.returnValue(of([])),
      create: jasmine.createSpy('create').and.returnValue(of({})),
      update: jasmine.createSpy('update').and.returnValue(of({})),
      delete: jasmine.createSpy('delete').and.returnValue(of({})),
    };

    mockMatDialog = {
      open: jasmine.createSpy('open').and.returnValue({
        afterClosed: () => of({ action: 'refresh', data: {} }),
      }),
    };

    mockRouter = {
      navigate: jasmine.createSpy('navigate'),
    };

    mockMatSnackBar = {
      open: jasmine.createSpy('open'),
    };

    await TestBed.configureTestingModule({
      declarations: [ListRestaurantComponent],
      imports: [HttpClientTestingModule, NoopAnimationsModule, MaterialModule, ConfirmationDialogComponent],
      providers: [
        { provide: RestaurantService, useValue: mockRestaurantService },
        { provide: MatDialog, useValue: mockMatDialog },
        { provide: Router, useValue: mockRouter },
        { provide: MatSnackBar, useValue: mockMatSnackBar },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(ListRestaurantComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should get all restaurants on init', () => {
    component.ngOnInit();
    expect(mockRestaurantService.getAll).toHaveBeenCalled();
  });

  it('should set loading to false and dataSource on getAllRestaurant success', () => {
    const dummyRestaurants = [{ id: 1, name: 'Restaurant 1', type: 1 }];
    mockRestaurantService.getAll.and.returnValue(of(dummyRestaurants));

    component.getAllRestaurant();

    expect(component.loading).toBe(false);
    expect(component.dataSource.length).toBe(1);
    expect(component.dataSource[0].displayType).toBe('Pure Veg');
  });

  it('should set loading to false and empty dataSource on getAllRestaurant error', () => {
    mockRestaurantService.getAll.and.returnValue(throwError('error'));

    component.getAllRestaurant();

    expect(component.loading).toBe(false);
    expect(component.dataSource.length).toBe(0);
  });

  it('should open the edit dialog and call createRestaurant on CREATE action', () => {
    component.openEditDialog('CREATE');
    expect(mockMatDialog.open).toHaveBeenCalled();
    expect(mockRestaurantService.create).toHaveBeenCalled();
  });

  it('should open the edit dialog and call updateRestaurant on UPDATE action', () => {
    const item = { id: 1 };
    component.openEditDialog('UPDATE', item);
    expect(mockMatDialog.open).toHaveBeenCalled();
    expect(mockRestaurantService.update).toHaveBeenCalledWith(item.id, {});
  });


  it('should navigate to menu on navigateToMenu', () => {
    const element = { id: 1 };
    component.navigateToMenu(element);
    expect(mockRouter.navigate).toHaveBeenCalledWith([
      'pages',
      'menu',
      element.id,
    ]);
  });

  it('should cleanup subscriptions on destroy', () => {
    const destroySpy = spyOn(component['destroy$'], 'next').and.callThrough();
    const completeSpy = spyOn(
      component['destroy$'],
      'complete'
    ).and.callThrough();

    component.ngOnDestroy();

    expect(destroySpy).toHaveBeenCalled();
    expect(completeSpy).toHaveBeenCalled();
  });
});
