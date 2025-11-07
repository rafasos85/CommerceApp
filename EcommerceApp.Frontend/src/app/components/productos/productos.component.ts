import { ChangeDetectionStrategy, Component, OnInit, inject,ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArticuloService } from '../../services/articulo.service';
import { CarritoService } from '../../services/carrito.service';
import { Articulo } from '../../models/articulo.model';


@Component({
  selector: 'app-productos',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './productos.component.html',
  styleUrls: ['./productos.component.css']
})
export class ProductosComponent implements OnInit {
  private articuloService = inject(ArticuloService);
  private carritoService = inject(CarritoService);
   private cdr = inject(ChangeDetectorRef);

  articulos: Articulo[] = [];
  loading = false;
  errorMessage = '';

  ngOnInit(): void {
    this.loadArticulos();
  }

  loadArticulos(): void {
    this.articuloService.getAll().subscribe({
      next: (data) => {
        this.articulos = data.filter(a => a.activo && a.stock > 0);
        this.loading = false;
        console.log("articulos cargados:", this.articulos);
        this.cdr.markForCheck(); // 👈 fuerza actualización de la vista
      },
      error: (error) => {
        this.errorMessage = 'Error al cargar los productos';
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  agregarAlCarrito(articulo: Articulo): void {
    this.carritoService.agregarItem({
      articuloId: articulo.id,
      cantidad: 1
    }).subscribe({
      next: () => {
        alert('Producto agregado al carrito');
      },
      error: (error) => {
        alert(error.error?.message || 'Error al agregar al carrito');
      }
    });
  }
}
