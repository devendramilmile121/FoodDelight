import { Component, OnDestroy, OnInit } from '@angular/core';
import { RestaurantService } from '../../services/restraurant.service';
import { Subject, takeUntil } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { CreateUpdateRestaurantDialogComponent } from '../create-update-restaurant-dialog/create-update-restaurant-dialog.component';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmationDialogComponent } from '../../common/confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-list-restaurant',
  templateUrl: './list-restaurant.component.html',
  styleUrl: './list-restaurant.component.css',
})
export class ListRestaurantComponent implements OnInit, OnDestroy {
  displayedColumns: string[] = [
    'name',
    'description',
    'location',
    'type',
    'phone',
    'email',
    ' ',
  ];
  dataSource: any[] = [];
  loading: boolean = true;

  private destroy$: Subject<string> = new Subject<string>();

  constructor(
    private rs: RestaurantService,
    private dialog: MatDialog,
    private router: Router,
    private snack: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.getAllRestaurant();
  }

  getAllRestaurant(): void {
    this.loading = true;
    this.rs
      .getAll()
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        (res) => {
          if (res && res?.length > 0) {
            this.dataSource = res.map((a: any) => {
              return {
                ...a,
                displayType: this.getType(a.type),
              };
            });
            this.loading = false;
          } else {
            this.dataSource = [];
            this.loading = false;
          }
        },
        (error) => {
          this.loading = false;
        }
      );
  }

  getType(id: number): string {
    return RESTAURANT_TYPES.get(id) || '';
  }

  openEditDialog(action: string, item?: any): void {
    const dialogRef = this.dialog.open(CreateUpdateRestaurantDialogComponent, {
      data: { restaurant: item, action: action },
      disableClose: true,
      width: '50%',
      height: '98%',
    });

    dialogRef
      .afterClosed()
      .pipe(takeUntil(this.destroy$))
      .subscribe((result) => {
        if (result.action === 'refresh') {
          if (action === 'CREATE') {
            this.createRestaurant(result.data);
          }
          if (action === 'UPDATE') {
            this.updateRestaurant(item.id, result.data);
          }
        }
      });
  }

  openConfirmation(element: any): void {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: {
        deleteMsg: `Are you sure you want to delete ${element.name}`,
        deleteTitle: 'Confirm Delete',
      },
      disableClose: true,
      width: '30%',
      height: '30%',
    });

    dialogRef
      .afterClosed()
      .pipe(takeUntil(this.destroy$))
      .subscribe((result) => {
        if (result === 'yes') {
          this.deleteRestaurant(element.id);
        }
      });
  }

  deleteRestaurant(id: any) {
    this.rs
      .delete(id)
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        (result) => {
          if (result) {
            this.getAllRestaurant();
            this.snack.open('Restaurant deleted successfully.', 'close');
          } else {
            this.snack.open('Failed to delete Restaurant.', 'close');
          }
        },
        (error) => {
          this.snack.open('Failed to delete Restaurant.', 'close');
        }
      );
  }

  updateRestaurant(id: any, data: any) {
    this.rs
      .update(id, data)
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        (result) => {
          if (result) {
            this.getAllRestaurant();
            this.snack.open('Restaurant updated successfully.', 'close');
          } else {
            this.snack.open('Failed to update Restaurant.', 'close');
          }
        },
        (error) => {
          this.snack.open('Failed to update Restaurant.', 'close');
        }
      );
  }

  createRestaurant(data: any) {
    this.rs
      .create(data)
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        (result) => {
          if (result) {
            this.getAllRestaurant();
            this.snack.open('Restaurant created successfully.', 'close');
          } else {
            this.snack.open('Failed to create Restaurant.', 'close');
          }
        },
        (error) => {
          this.snack.open('Failed to create Restaurant.', 'close');
        }
      );
  }

  ngOnDestroy(): void {
    this.destroy$.next('');
    this.destroy$.complete();
  }

  navigateToMenu(element: any): void {
    this.router.navigate(['pages', 'menu', element.id]);
  }
}

export const RESTAURANT_TYPES = new Map<number, string>();
RESTAURANT_TYPES.set(1, 'Pure Veg');
RESTAURANT_TYPES.set(2, 'Non Veg');
RESTAURANT_TYPES.set(3, 'Veg + Non Veg');
