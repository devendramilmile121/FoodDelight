import { TestBed } from '@angular/core/testing';
import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';
import { MenuItemService } from './menu-item.service';

describe('MenuItemService', () => {
  let service: MenuItemService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [MenuItemService],
    });
    service = TestBed.inject(MenuItemService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should retrieve all menu items by menuId', () => {
    const menuId = '1';
    const dummyMenuItems = [
      { id: '1', name: 'Item 1' },
      { id: '2', name: 'Item 2' },
    ];

    service.getAll(menuId).subscribe((items) => {
      expect(items.length).toBe(2);
      expect(items).toEqual(dummyMenuItems);
    });

    const req = httpMock.expectOne(`${service['baseUrl']}/menus/${menuId}`);
    expect(req.request.method).toBe('GET');
    req.flush(dummyMenuItems);
  });

  it('should create a new menu item', () => {
    const menuId = '1';
    const newItem = { name: 'New Item' };
    const createdItem = { id: '3', ...newItem };

    service.create(menuId, newItem).subscribe((item) => {
      expect(item).toEqual(createdItem);
    });

    const req = httpMock.expectOne(`${service['baseUrl']}/${menuId}`);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(newItem);
    req.flush(createdItem);
  });

  it('should update an existing menu item', () => {
    const menuId = '1';
    const itemId = '2';
    const updatedItem = { name: 'Updated Item' };

    service.update(menuId, itemId, updatedItem).subscribe((item) => {
      expect(item).toEqual(updatedItem);
    });

    const req = httpMock.expectOne(`${service['baseUrl']}/${menuId}/${itemId}`);
    expect(req.request.method).toBe('PUT');
    expect(req.request.body).toEqual(updatedItem);
    req.flush(updatedItem);
  });

  it('should delete a menu item by id', () => {
    const itemId = '1';

    service.delete(itemId).subscribe((response) => {
      expect(response).toEqual(null);
    });

    const req = httpMock.expectOne(`${service['baseUrl']}/${itemId}`);
    expect(req.request.method).toBe('DELETE');
    req.flush(null);
  });
});
