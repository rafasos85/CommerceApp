import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Tienda, TiendaCreate } from '../models/tienda.model';

@Injectable({
  providedIn: 'root'
})
export class TiendaService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7272/api/tiendas';

  getAll(): Observable<Tienda[]> {
    return this.http.get<Tienda[]>(this.apiUrl);
  }

  getById(id: number): Observable<Tienda> {
    return this.http.get<Tienda>(`${this.apiUrl}/${id}`);
  }

  create(tienda: TiendaCreate): Observable<Tienda> {
    return this.http.post<Tienda>(this.apiUrl, tienda);
  }

  update(id: number, tienda: Partial<Tienda>): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, tienda);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
