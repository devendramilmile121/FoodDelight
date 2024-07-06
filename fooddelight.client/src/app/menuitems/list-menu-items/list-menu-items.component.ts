import { Component } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { MenuItemService } from '../../services/menu-item.service';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CreateUpdateMenuItemComponent } from '../create-update-menu-item/create-update-menu-item.component';
import { ConfirmationDialogComponent } from '../../common/confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-list-menu-items',
  templateUrl: './list-menu-items.component.html',
  styleUrl: './list-menu-items.component.css',
})
export class ListMenuItemsComponent {
  displayedColumns: string[] = ['name', 'description', 'price', 'imagePath', ' '];
  dataSource: any[] = [];

  private destroy$: Subject<string> = new Subject<string>();
  menuId: string | null = '0';
  loading: boolean = true;
  constructor(
    private mis: MenuItemService,
    private dialog: MatDialog,
    private router: Router,
    private route: ActivatedRoute,
    private snack: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.route.paramMap.pipe(takeUntil(this.destroy$)).subscribe((params) => {
      this.menuId = params.get('id');
      if (this.menuId) {
        this.getAllMenus(this.menuId);
      }
    });
  }

  getAllMenus(menuId: string): void {
    this.loading = true;
    this.mis
      .getAll(menuId)
      .pipe(takeUntil(this.destroy$))
      .subscribe((res) => {
        if (res && res?.length > 0) {
          this.dataSource = res;
          this.loading = false;
        } else {
          this.dataSource = [];
          this.loading = false;
        }
      }, (error) => {
        this.loading = false;
      });
  }

  openEditDialog(action: string, item?: any): void {
    const dialogRef = this.dialog.open(CreateUpdateMenuItemComponent, {
      data: { restaurant: item, action: action },
      disableClose: true,
      width: '50%',
      height: '87%',
    });

    dialogRef
      .afterClosed()
      .pipe(takeUntil(this.destroy$))
      .subscribe((result) => {
        if (result.action === 'refresh') {
          if (action === 'CREATE' && this.menuId) {
            this.createMenu(this.menuId, result.data);
          }
          if (action === 'UPDATE' && this.menuId) {
            this.updateMenu(this.menuId, item.id, result.data);
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
    this.mis
      .delete(id)
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        (result) => {
          if (result) {
            if (this.menuId) {
              this.getAllMenus(this.menuId);
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

  updateMenu(menuId: string, id: any, data: any) {
    this.mis
      .update(menuId, id, data)
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        (result) => {
          if (result) {
            if (this.menuId) {
              this.getAllMenus(this.menuId);
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

  createMenu(menuId: string, data: any) {
    this.mis
      .create(menuId, data)
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        (result) => {
          if (result) {
            if (this.menuId) {
              this.getAllMenus(this.menuId);
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
    this.router.navigate(['pages', 'menu-items', element.id]);
  }

  back(): void {
    this.router.navigate(['pages', 'menu', this.menuId]);
  }
}
