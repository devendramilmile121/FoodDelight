import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { MenuService } from '../../services/menu.service';
import { CreateUpdateMenuDialogComponent } from '../create-update-menu-dialog/create-update-menu-dialog.component';
import { ConfirmationDialogComponent } from '../../common/confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-menu-listing',
  templateUrl: './menu-listing.component.html',
  styleUrl: './menu-listing.component.css',
})
export class MenuListingComponent {
  displayedColumns: string[] = ['name', 'type', 'count', ' '];
  dataSource: any[] = [];

  private destroy$: Subject<string> = new Subject<string>();
  restaurantId: string | null = '0';

  constructor(
    private ms: MenuService,
    private dialog: MatDialog,
    private router: Router,
    private route: ActivatedRoute,
    private snack: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.route.paramMap.pipe(takeUntil(this.destroy$)).subscribe((params) => {
      this.restaurantId = params.get('id');
      if (this.restaurantId) {
        this.getAllMenus(this.restaurantId);
      }
    });
  }

  getAllMenus(restaurantId: string): void {
    this.ms
      .getAll(restaurantId)
      .pipe(takeUntil(this.destroy$))
      .subscribe((res) => {
        if (res && res?.length > 0) {
          this.dataSource = res.map((a: any) => {
            return {
              ...a,
              displayType: this.getType(a.menuType),
            };
          });
        } else {
          this.dataSource = [];
        }
      });
  }

  getType(id: number): string {
    return MENU_TYPES.get(id) || '';
  }

  openEditDialog(action: string, item?: any): void {
    const dialogRef = this.dialog.open(CreateUpdateMenuDialogComponent, {
      data: { restaurant: item, action: action },
      disableClose: true,
      width: '50%',
      height: '41%',
    });

    dialogRef
      .afterClosed()
      .pipe(takeUntil(this.destroy$))
      .subscribe((result) => {
        if (result.action === 'refresh') {
          if (action === 'CREATE' && this.restaurantId) {
            this.createMenu(this.restaurantId, result.data);
          }
          if (action === 'UPDATE' && this.restaurantId) {
            this.updateMenu(this.restaurantId, item.id, result.data);
          }
        }
      });
  }

  openConfirmation(element: any): void {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: {
        deleteMsg: `Are you sure you want to delete ${element.name}?`,
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
          this.deleteMenu(element.id);
        }
      });
  }

  deleteMenu(id: any) {
    this.ms
      .delete(id)
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        (result) => {
          if (result) {
            if (this.restaurantId) {
              this.getAllMenus(this.restaurantId);
            }
            this.snack.open('Menu deleted successfully.', 'close');
          } else {
            this.snack.open('Failed to delete Menu.', 'close');
          }
        },
        (error) => {
          this.snack.open('Failed to delete Menu.', 'close');
        }
      );
  }

  updateMenu(restaurantId: string, id: any, data: any) {
    this.ms
      .update(restaurantId, id, data)
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        (result) => {
          if (result) {
            if (this.restaurantId) {
              this.getAllMenus(this.restaurantId);
            }
            this.snack.open('Menu updated successfully.', 'close');
          } else {
            this.snack.open('Failed to update Menu.', 'close');
          }
        },
        (error) => {
          this.snack.open('Failed to update Menu.', 'close');
        }
      );
  }

  createMenu(restaurantId: string,data: any) {
    this.ms
      .create(restaurantId,data)
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        (result) => {
          if (result) {
            if (this.restaurantId) {
              this.getAllMenus(this.restaurantId);
            }
            this.snack.open('Menu created successfully.', 'close');
          } else {
            this.snack.open('Failed to create Menu.', 'close');
          }
        },
        (error) => {
          this.snack.open('Failed to create Menu.', 'close');
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

export const MENU_TYPES = new Map<number, string>();
MENU_TYPES.set(1, 'Veg');
MENU_TYPES.set(2, 'Non Veg');
