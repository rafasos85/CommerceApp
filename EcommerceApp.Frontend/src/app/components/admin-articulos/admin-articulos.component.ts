import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ArticuloService } from '../../services/articulo.service';
import { TiendaService } from '../../services/tienda.service';
import { ArticuloCreate, Articulo } from '../../models/articulo.model';
import { Tienda } from '../../models/tienda.model';

@Component({
  selector: 'app-admin-articulos',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-articulos.component.html',
  styleUrls: ['./admin-articulos.component.css']
})
export class AdminArticulosComponent implements OnInit {
  private articuloService = inject(ArticuloService);
  private tiendaService = inject(TiendaService);

  articulo: ArticuloCreate = {
    codigo: '',
    descripcion: '',
    precio: 0,
    imagen: '',
    stock: 0
  };

  articulos: Articulo[] = [];
  tiendas: Tienda[] = [];
  
  // Para asignar artículo a tienda
  selectedArticuloId: number = 0;
  selectedTiendaId: number = 0;
  stockTienda: number = 0;

  successMessage = '';
  errorMessage = '';
  loading = false;
  loadingArticulos = false;

  ngOnInit(): void {
    this.loadArticulos();
    this.loadTiendas();
  }

  loadArticulos(): void {
    this.articuloService.getAll().subscribe({
      next: (data) => {
        this.articulos = data;
        this.loadingArticulos = false;
        console.log('Artículos cargados:', data);
      },
      error: (error) => {
        this.errorMessage = 'Error al cargar artículos';
        this.loadingArticulos = false;
      }
    });
  }

  loadTiendas(): void {
    this.tiendaService.getAll().subscribe({
      next: (data) => {
        this.tiendas = data;
      },
      error: (error) => {
        console.error('Error al cargar tiendas', error);
      }
    });
  }

  onSubmit(): void {
    this.loading = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.articuloService.create(this.articulo).subscribe({
      next: (response) => {
        this.successMessage = 'Artículo creado exitosamente';
        this.resetForm();
        this.loadArticulos();
        this.loading = false;
      },
      error: (error) => {
        this.errorMessage = error.error?.message || 'Error al crear el artículo';
        this.loading = false;
      }
    });
  }

  asignarATienda(): void {
    if (!this.selectedArticuloId || !this.selectedTiendaId) {
      this.errorMessage = 'Debe seleccionar un artículo y una tienda';
      return;
    }

    const dto = {
      articuloId: this.selectedArticuloId,
      tiendaId: this.selectedTiendaId,
      stockTienda: this.stockTienda
    };

    this.articuloService.asignarATienda(dto).subscribe({
      next: () => {
        this.successMessage = 'Artículo asignado a la tienda exitosamente';
        this.selectedArticuloId = 0;
        this.selectedTiendaId = 0;
        this.stockTienda = 0;
      },
      error: (error) => {
        this.errorMessage = error.error?.message || 'Error al asignar artículo a tienda';
      }
    });
  }

  deleteArticulo(id: number): void {
    if (!confirm('¿Está seguro de eliminar este artículo?')) {
      return;
    }

    this.articuloService.delete(id).subscribe({
      next: () => {
        this.successMessage = 'Artículo eliminado exitosamente';
        this.loadArticulos();
      },
      error: (error) => {
        this.errorMessage = error.error?.message || 'Error al eliminar el artículo';
      }
    });
  }

  resetForm(): void {
    this.articulo = {
      codigo: '',
      descripcion: '',
      precio: 0,
      imagen: '',
      stock: 0
    };
  }

  clearMessages(): void {
    this.successMessage = '';
    this.errorMessage = '';
  }
}
