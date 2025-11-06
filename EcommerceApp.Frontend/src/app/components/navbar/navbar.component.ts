import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CarritoService } from '../../services/carrito.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  authService = inject(AuthService);
  carritoService = inject(CarritoService);
  private router = inject(Router);

  currentUser$ = this.authService.currentUser$;
  carrito$ = this.carritoService.carrito$;

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  get itemCount(): number {
    return this.carritoService.itemCount;
  }
}
