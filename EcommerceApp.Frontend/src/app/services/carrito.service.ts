import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject, tap } from 'rxjs';
import { Carrito, AgregarItemCarrito } from '../models/carrito.model';

@Injectable({
  providedIn: 'root'
})
export class CarritoService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7272/api/carrito';
  private carritoSubject = new BehaviorSubject<Carrito | null>(null);
  public carrito$ = this.carritoSubject.asObservable();

  getCarritoActivo(): Observable<Carrito> {
    return this.http.get<Carrito>(this.apiUrl).pipe(
      tap(carrito => this.carritoSubject.next(carrito))
    );
  }

  agregarItem(item: AgregarItemCarrito): Observable<Carrito> {
    return this.http.post<Carrito>(`${this.apiUrl}/agregar-item`, item).pipe(
      tap(carrito => this.carritoSubject.next(carrito))
    );
  }

  actualizarItem(itemId: number, cantidad: number): Observable<Carrito> {
    return this.http.put<Carrito>(`${this.apiUrl}/actualizar-item`, { itemId, cantidad }).pipe(
      tap(carrito => this.carritoSubject.next(carrito))
    );
  }

  removerItem(itemId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/remover-item/${itemId}`).pipe(
      tap(() => this.getCarritoActivo().subscribe())
    );
  }

  completarCompra(): Observable<any> {
    return this.http.post(`${this.apiUrl}/completar-compra`, {}).pipe(
      tap(() => this.carritoSubject.next(null))
    );
  }

  get itemCount(): number {
    const carrito = this.carritoSubject.value;
    return carrito?.items?.reduce((sum, item) => sum + item.cantidad, 0) || 0;
  }
}
