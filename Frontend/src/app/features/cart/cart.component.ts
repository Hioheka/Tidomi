import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { CartService } from '../../core/services/cart.service';
import { Cart } from '../../core/models/cart.model';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, TranslateModule],
  template: `
    <div class="cart-container">
      <h2>{{ 'cart.title' | translate }}</h2>

      <div *ngIf="cart.items.length === 0" class="empty">
        {{ 'cart.empty' | translate }}
      </div>

      <div *ngIf="cart.items.length > 0">
        <div *ngFor="let item of cart.items" class="cart-item">
          <h4>{{ item.service.name }}</h4>
          <div class="item-details">
            <span>{{ 'cart.quantity' | translate }}: {{ item.quantity }}</span>
            <span>{{ item.service.price }} x {{ item.quantity }} = {{ item.service.price * item.quantity }}</span>
          </div>
          <button (click)="removeItem(item.service.id)" class="btn-remove">{{ 'cart.remove' | translate }}</button>
        </div>

        <div class="cart-total">
          <h3>{{ 'cart.total' | translate }}: {{ cart.totalAmount }}</h3>
          <button (click)="checkout()" class="btn-checkout">{{ 'cart.checkout' | translate }}</button>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .cart-container { padding: 2rem; max-width: 800px; margin: 0 auto; }
    .empty { text-align: center; padding: 3rem; color: #666; }
    .cart-item { border: 1px solid #ddd; border-radius: 8px; padding: 1.5rem; margin-bottom: 1rem; }
    .item-details { display: flex; justify-content: space-between; margin: 1rem 0; }
    .btn-remove { background: #dc3545; color: white; border: none; padding: 0.5rem 1rem; border-radius: 4px; cursor: pointer; }
    .cart-total { margin-top: 2rem; text-align: right; }
    .btn-checkout { background: #007bff; color: white; border: none; padding: 0.75rem 2rem; border-radius: 4px; cursor: pointer; font-size: 1rem; }
  `]
})
export class CartComponent implements OnInit {
  cart: Cart = { items: [], totalAmount: 0 };

  constructor(private cartService: CartService, private router: Router) {}

  ngOnInit() {
    this.cartService.cart$.subscribe(cart => this.cart = cart);
  }

  removeItem(serviceId: string) {
    this.cartService.removeFromCart(serviceId);
  }

  checkout() {
    this.router.navigate(['/booking']);
  }
}
