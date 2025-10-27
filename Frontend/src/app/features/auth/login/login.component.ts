import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule, TranslateModule],
  template: `
    <div class="auth-container">
      <h2>{{ 'auth.login.title' | translate }}</h2>
      <form (ngSubmit)="onSubmit()" #loginForm="ngForm">
        <div class="form-group">
          <label>{{ 'auth.login.email' | translate }}</label>
          <input type="email" [(ngModel)]="credentials.email" name="email" required>
        </div>
        <div class="form-group">
          <label>{{ 'auth.login.password' | translate }}</label>
          <input type="password" [(ngModel)]="credentials.password" name="password" required>
        </div>
        <button type="submit" [disabled]="!loginForm.valid">{{ 'auth.login.submit' | translate }}</button>
        <div class="error" *ngIf="error">{{ error }}</div>
      </form>
      <p>
        {{ 'auth.login.noAccount' | translate }}
        <a routerLink="/auth/register">{{ 'auth.login.registerLink' | translate }}</a>
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
export class LoginComponent {
  credentials = { email: '', password: '' };
  error = '';

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    this.authService.login(this.credentials).subscribe({
      next: () => this.router.navigate(['/']),
      error: (err) => this.error = err.error?.message || 'Login failed'
    });
  }
}
