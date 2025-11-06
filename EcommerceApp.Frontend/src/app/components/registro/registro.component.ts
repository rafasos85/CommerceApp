import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { ClienteCreate } from '../../models/cliente.model';

@Component({
  selector: 'app-registro',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent {
  private authService = inject(AuthService);
  private router = inject(Router);

  cliente: ClienteCreate = {
    nombre: '',
    apellidos: '',
    direccion: '',
    email: '',
    password: ''
  };

  confirmPassword = '';
  errorMessage = '';
  successMessage = '';
  loading = false;

  onSubmit(): void {
    if (this.cliente.password !== this.confirmPassword) {
      this.errorMessage = 'Las contraseñas no coinciden';
      return;
    }

    this.loading = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.authService.register(this.cliente).subscribe({
      next: () => {
        this.successMessage = 'Registro exitoso. Redirigiendo al login...';
        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 2000);
      },
      error: (error) => {
        this.errorMessage = error.error?.message || 'Error al registrarse';
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      }
    });
  }
}
