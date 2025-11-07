import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Articulo, ArticuloCreate } from '../models/articulo.model';

@Injectable({
  providedIn: 'root'
})
export class ArticuloService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7272/api/articulos';

  getAll(): Observable<Articulo[]> {
    return this.http.get<Articulo[]>(this.apiUrl);
  }

  getById(id: number): Observable<Articulo> {
    return this.http.get<Articulo>(`${this.apiUrl}/${id}`);
  }

  getByTiendaId(tiendaId: number): Observable<Articulo[]> {
    return this.http.get<Articulo[]>(`${this.apiUrl}/tienda/${tiendaId}`);
  }

  create(articulo: ArticuloCreate): Observable<Articulo> {
    return this.http.post<Articulo>(this.apiUrl, articulo);
  }

  update(id: number, articulo: Partial<Articulo>): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, articulo);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  asignarATienda(dto: { articuloId: number; tiendaId: number; stockTienda: number }): Observable<any> {
    return this.http.post(`${this.apiUrl}/asignar-tienda`, dto);
  }
}
