import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({providedIn: 'root'})
export class MenuItemService {
  private baseUrl: string = "http://localhost:5132/api/MenuItem"
  constructor(private http: HttpClient) { }

  getAll(menuId: string): Observable<any> {
    return this.http.get(`${this.baseUrl}/menus/${menuId}`);
  }

  create(menuId: string, payload: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/${menuId}`, payload);
  }
  update(menuId: string, id: string, payload: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/${menuId}/${id}`, payload);
  }
  delete(id: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}
