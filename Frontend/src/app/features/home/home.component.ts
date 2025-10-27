import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterModule, TranslateModule],
  template: `
    <div class="home-container">
      <h1>{{ 'app.title' | translate }}</h1>
      <p>Welcome to Tidomi - Your Professional Cleaning Service Platform</p>

      <div class="features">
        <div class="feature">
          <h3>Easy Booking</h3>
          <p>Book cleaning services with just a few clicks</p>
        </div>
        <div class="feature">
          <h3>Professional Staff</h3>
          <p>Experienced and vetted cleaning professionals</p>
        </div>
        <div class="feature">
          <h3>Flexible Scheduling</h3>
          <p>Choose the date and time that works for you</p>
        </div>
      </div>

      <div class="cta">
        <a routerLink="/services" class="btn btn-primary">Browse Services</a>
        <a routerLink="/auth/register" class="btn btn-secondary">Get Started</a>
      </div>
    </div>
  `,
  styles: [`
    .home-container { padding: 2rem; text-align: center; }
    .features { display: grid; grid-template-columns: repeat(auto-fit, minmax(250px, 1fr)); gap: 2rem; margin: 2rem 0; }
    .feature { padding: 1.5rem; border: 1px solid #ddd; border-radius: 8px; }
    .cta { margin-top: 2rem; }
    .btn { padding: 0.75rem 1.5rem; margin: 0 0.5rem; text-decoration: none; border-radius: 4px; display: inline-block; }
    .btn-primary { background: #007bff; color: white; }
    .btn-secondary { background: #6c757d; color: white; }
  `]
})
export class HomeComponent {}
