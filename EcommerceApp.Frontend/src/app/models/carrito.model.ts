export interface Carrito {
  id: number;
  clienteId: number;
  fechaCreacion: Date;
  total: number;
  items: CarritoItem[];
}

export interface CarritoItem {
  id: number;
  articuloId: number;
  articuloDescripcion: string;
  cantidad: number;
  precioUnitario: number;
  subtotal: number;
}

export interface AgregarItemCarrito {
  articuloId: number;
  cantidad: number;
}
