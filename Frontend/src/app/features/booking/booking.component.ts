import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { BookingService } from '../../core/services/booking.service';
import { CartService } from '../../core/services/cart.service';

@Component({
  selector: 'app-booking',
  standalone: true,
  imports: [CommonModule, FormsModule, TranslateModule],
  template: `
    <div class="booking-container">
      <h2>{{ 'booking.title' | translate }}</h2>
      <form (ngSubmit)="onSubmit()" #bookingForm="ngForm">
        <div class="form-group">
          <label>{{ 'booking.scheduledDate' | translate }}</label>
          <input type="date" [(ngModel)]="booking.scheduledDate" name="scheduledDate" required>
        </div>
        <div class="form-group">
          <label>{{ 'booking.scheduledTime' | translate }}</label>
          <input type="time" [(ngModel)]="booking.scheduledTime" name="scheduledTime" required>
        </div>
        <div class="form-group">
          <label>{{ 'booking.address' | translate }}</label>
          <textarea [(ngModel)]="booking.address" name="address" required rows="3"></textarea>
        </div>
        <div class="form-group">
          <label>{{ 'booking.city' | translate }}</label>
          <input type="text" [(ngModel)]="booking.city" name="city" required>
        </div>
        <div class="form-group">
          <label>{{ 'booking.district' | translate }}</label>
          <input type="text" [(ngModel)]="booking.district" name="district" required>
        </div>
        <div class="form-group">
          <label>{{ 'booking.postalCode' | translate }}</label>
          <input type="text" [(ngModel)]="booking.postalCode" name="postalCode" required>
        </div>
        <div class="form-group">
          <label>{{ 'booking.notes' | translate }}</label>
          <textarea [(ngModel)]="booking.notes" name="notes" rows="3"></textarea>
        </div>
        <button type="submit" [disabled]="!bookingForm.valid">{{ 'booking.submit' | translate }}</button>
      </form>
    </div>
  `,
  styles: [`
    .booking-container { max-width: 600px; margin: 2rem auto; padding: 2rem; }
    .form-group { margin-bottom: 1.5rem; }
    .form-group label { display: block; margin-bottom: 0.5rem; font-weight: bold; }
    .form-group input, .form-group textarea { width: 100%; padding: 0.75rem; border: 1px solid #ddd; border-radius: 4px; }
    button { width: 100%; padding: 1rem; background: #007bff; color: white; border: none; border-radius: 4px; cursor: pointer; font-size: 1rem; }
    button:disabled { opacity: 0.5; }
  `]
})
export class BookingComponent {
  booking = {
    scheduledDate: '',
    scheduledTime: '',
    address: '',
    city: '',
    district: '',
    postalCode: '',
    notes: '',
    items: []
  };

  constructor(
    private bookingService: BookingService,
    private cartService: CartService,
    private router: Router
  ) {}

  onSubmit() {
    const cart = this.cartService.getCart();
    const bookingRequest = {
      ...this.booking,
      scheduledDate: new Date(this.booking.scheduledDate),
      items: cart.items.map(item => ({
        serviceId: item.service.id,
        quantity: item.quantity,
        notes: item.notes
      }))
    };

    this.bookingService.createBooking(bookingRequest).subscribe({
      next: () => {
        this.cartService.clearCart();
        this.router.navigate(['/payment']);
      },
      error: (err) => alert('Booking failed: ' + (err.error?.message || 'Unknown error'))
    });
  }
}
