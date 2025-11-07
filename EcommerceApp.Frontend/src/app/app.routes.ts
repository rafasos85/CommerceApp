import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegistroComponent } from './components/registro/registro.component';
import { ProductosComponent } from './components/productos/productos.component';
import { CarritoComponent } from './components/carrito/carrito.component';
import { AdminArticulosComponent } from './components/admin-articulos/admin-articulos.component';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'registro', component: RegistroComponent },
  { 
    path: 'productos', 
    component: ProductosComponent,
    canActivate: [authGuard]
  },
  { 
    path: 'carrito', 
    component: CarritoComponent,
    canActivate: [authGuard]
  },
  { 
    path: 'admin/articulos', 
    component: AdminArticulosComponent,
    canActivate: [authGuard]
  }
];
