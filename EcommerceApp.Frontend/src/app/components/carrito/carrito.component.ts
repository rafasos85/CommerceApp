import { Component, OnInit, inject ,ChangeDetectorRef} from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { CarritoService } from '../../services/carrito.service';
import { Carrito } from '../../models/carrito.model';

@Component({
  selector: 'app-carrito',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './carrito.component.html',
  styleUrls: ['./carrito.component.css']
})
export class CarritoComponent implements OnInit {
  private carritoService = inject(CarritoService);
  private router = inject(Router);
   private cdr = inject(ChangeDetectorRef);

  carrito: Carrito | null = null;
  loading = true;

  // Devuelve la suma de las cantidades de todos los items en el carrito
  get totalItems(): number {
    return this.carrito?.items?.reduce((sum, item) => sum + (item.cantidad ?? 0), 0) ?? 0;
  }

  ngOnInit(): void {
    this.loadCarrito();
  }

  loadCarrito(): void {
    this.carritoService.getCarritoActivo().subscribe({
      next: (data) => {
        this.carrito = data;
        this.loading = false;
         this.cdr.markForCheck(); 
      },
      error: () => {
        this.loading = false;
         this.cdr.markForCheck(); 
      }
    });
  }

  actualizarCantidad(itemId: number, cantidad: number): void {
    if (cantidad < 1) return;
    
    this.carritoService.actualizarItem(itemId, cantidad).subscribe({
      next: (data) => {
        this.carrito = data;
         this.cdr.markForCheck(); 
      },
      error: (error) => {
        alert(error.error?.message || 'Error al actualizar cantidad');
         this.cdr.markForCheck(); 
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
         this.cdr.markForCheck();
      },
      error: (error) => {
        alert(error.error?.message || 'Error al completar la compra');
      }
    });
  }
}
