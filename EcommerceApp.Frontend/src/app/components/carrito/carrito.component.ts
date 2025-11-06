import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { CarritoService } from '../../services/carrito.service';
import { Carrito } from '../../models/carrito.model';

@Component({
  selector: 'app-carrito',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './carrito.component.html',
  styleUrls: ['./carrito.component.css']
})
export class CarritoComponent implements OnInit {
  private carritoService = inject(CarritoService);
  private router = inject(Router);

  carrito: Carrito | null = null;
  loading = true;

  ngOnInit(): void {
    this.loadCarrito();
  }

  loadCarrito(): void {
    this.carritoService.getCarritoActivo().subscribe({
      next: (data) => {
        this.carrito = data;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  actualizarCantidad(itemId: number, cantidad: number): void {
    if (cantidad < 1) return;
    
    this.carritoService.actualizarItem(itemId, cantidad).subscribe({
      next: (data) => {
        this.carrito = data;
      },
      error: (error) => {
        alert(error.error?.message || 'Error al actualizar cantidad');
      }
    });
  }

  removerItem(itemId: number): void {
    if (!confirm('¿Desea eliminar este producto del carrito?')) return;

    this.carritoService.removerItem(itemId).subscribe({
      next: () => {
        this.loadCarrito();
      },
      error: (error) => {
        alert(error.error?.message || 'Error al remover item');
      }
    });
  }

  completarCompra(): void {
    if (!confirm('¿Confirmar compra?')) return;

    this.carritoService.completarCompra().subscribe({
      next: () => {
        alert('¡Compra completada exitosamente!');
        this.router.navigate(['/productos']);
      },
      error: (error) => {
        alert(error.error?.message || 'Error al completar la compra');
      }
    });
  }
}
