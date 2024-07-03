import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { MenuService } from './menu.service';

describe('MenuService', () => {
  let service: MenuService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [MenuService]
    });
    service = TestBed.inject(MenuService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should retrieve all menu items by restaurantId', () => {
    const restaurantId = '1';
    const dummyMenus = [{ id: '1', name: 'Menu 1' }, { id: '2', name: 'Menu 2' }];

    service.getAll(restaurantId).subscribe(menus => {
      expect(menus.length).toBe(2);
      expect(menus).toEqual(dummyMenus);
    });

    const req = httpMock.expectOne(`${service['baseUrl']}/Restaurant/${restaurantId}`);
    expect(req.request.method).toBe('GET');
    req.flush(dummyMenus);
  });

  it('should create a new menu', () => {
    const restaurantId = '1';
    const newMenu = { name: 'New Menu' };
    const createdMenu = { id: '3', ...newMenu };

    service.create(restaurantId, newMenu).subscribe(menu => {
      expect(menu).toEqual(createdMenu);
    });

    const req = httpMock.expectOne(`${service['baseUrl']}/${restaurantId}`);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(newMenu);
    req.flush(createdMenu);
  });

  it('should update an existing menu', () => {
    const restaurantId = '1';
    const menuId = '2';
    const updatedMenu = { name: 'Updated Menu' };

    service.update(restaurantId, menuId, updatedMenu).subscribe(menu => {
      expect(menu).toEqual(updatedMenu);
    });

    const req = httpMock.expectOne(`${service['baseUrl']}/${restaurantId}/${menuId}`);
    expect(req.request.method).toBe('PUT');
    expect(req.request.body).toEqual(updatedMenu);
    req.flush(updatedMenu);
  });

  it('should delete a menu by id', () => {
    const menuId = '1';

    service.delete(menuId).subscribe(response => {
      expect(response).toEqual(null);
    });

    const req = httpMock.expectOne(`${service['baseUrl']}/${menuId}`);
    expect(req.request.method).toBe('DELETE');
    req.flush(null);
  });
});
