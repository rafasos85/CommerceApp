export interface Cliente {
  id: number;
  nombre: string;
  apellidos: string;
  direccion: string;
  email: string;
}

export interface ClienteCreate {
  nombre: string;
  apellidos: string;
  direccion: string;
  email: string;
  password: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  cliente: Cliente;
}
