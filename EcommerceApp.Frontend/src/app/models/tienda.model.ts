export interface Tienda {
  id: number;
  sucursal: string;
  direccion: string;
  telefono: string;
  activo: boolean;
}

export interface TiendaCreate {
  sucursal: string;
  direccion: string;
  telefono: string;
}
