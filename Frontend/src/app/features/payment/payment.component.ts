import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-payment',
  standalone: true,
  imports: [CommonModule, RouterModule, TranslateModule],
  template: `
    <div class="payment-container">
      <h2>{{ 'payment.title' | translate }}</h2>
      <div class="todo-notice">
        <p>{{ 'payment.todo' | translate }}</p>
        <p>This is a placeholder for payment integration.</p>
        <p>In the next phase, payment gateways like iyzico, PayTR, or Stripe will be integrated here.</p>
      </div>
      <a routerLink="/booking" class="btn-back">{{ 'payment.backToBookings' | translate }}</a>
    </div>
  `,
  styles: [`
    .payment-container { max-width: 600px; margin: 2rem auto; padding: 2rem; text-align: center; }
    .todo-notice { background: #fff3cd; border: 1px solid #ffc107; border-radius: 8px; padding: 2rem; margin: 2rem 0; }
    .btn-back { display: inline-block; padding: 0.75rem 2rem; background: #007bff; color: white; text-decoration: none; border-radius: 4px; margin-top: 1rem; }
  `]
})
export class PaymentComponent {}
