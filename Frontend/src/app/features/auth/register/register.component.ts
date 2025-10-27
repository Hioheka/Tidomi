import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule, TranslateModule],
  template: `
    <div class="auth-container">
      <h2>{{ 'auth.register.title' | translate }}</h2>
      <form (ngSubmit)="onSubmit()" #registerForm="ngForm">
        <div class="form-group">
          <label>{{ 'auth.register.firstName' | translate }}</label>
          <input type="text" [(ngModel)]="userData.firstName" name="firstName" required>
        </div>
        <div class="form-group">
          <label>{{ 'auth.register.lastName' | translate }}</label>
          <input type="text" [(ngModel)]="userData.lastName" name="lastName" required>
        </div>
        <div class="form-group">
          <label>{{ 'auth.register.email' | translate }}</label>
          <input type="email" [(ngModel)]="userData.email" name="email" required>
        </div>
        <div class="form-group">
          <label>{{ 'auth.register.phoneNumber' | translate }}</label>
          <input type="tel" [(ngModel)]="userData.phoneNumber" name="phoneNumber" required>
        </div>
        <div class="form-group">
          <label>{{ 'auth.register.password' | translate }}</label>
          <input type="password" [(ngModel)]="userData.password" name="password" required>
        </div>
        <button type="submit" [disabled]="!registerForm.valid">{{ 'auth.register.submit' | translate }}</button>
        <div class="error" *ngIf="error">{{ error }}</div>
      </form>
      <p>
        {{ 'auth.register.haveAccount' | translate }}
        <a routerLink="/auth/login">{{ 'auth.register.loginLink' | translate }}</a>
      </p>
    </div>
  `,
  styles: [`
    .auth-container { max-width: 400px; margin: 2rem auto; padding: 2rem; border: 1px solid #ddd; border-radius: 8px; }
    .form-group { margin-bottom: 1rem; }
    .form-group label { display: block; margin-bottom: 0.5rem; }
    .form-group input { width: 100%; padding: 0.5rem; border: 1px solid #ddd; border-radius: 4px; }
    button { width: 100%; padding: 0.75rem; background: #007bff; color: white; border: none; border-radius: 4px; cursor: pointer; }
    button:disabled { opacity: 0.5; }
    .error { color: red; margin-top: 1rem; }
  `]
})
export class RegisterComponent {
  userData = { email: '', password: '', firstName: '', lastName: '', phoneNumber: '' };
  error = '';

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    this.authService.register(this.userData).subscribe({
      next: () => this.router.navigate(['/']),
      error: (err) => this.error = err.error?.message || 'Registration failed'
    });
  }
}
