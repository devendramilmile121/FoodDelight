import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({providedIn: 'root'})
export class MenuService {
  private baseUrl: string = "http://localhost:5132/api/Menu"
  constructor(private http: HttpClient) { }

  getAll(restaurantId: string): Observable<any> {
    return this.http.get(`${this.baseUrl}/Restaurant/${restaurantId}`);
  }

  create(restaurantId: string, payload: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/${restaurantId}`, payload);
  }
  update(restaurantId: string, id: string, payload: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/${restaurantId}/${id}`, payload);
  }
  delete(id: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}
