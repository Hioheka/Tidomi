import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Cart, CartItem } from '../models/cart.model';
import { Service } from '../models/service.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cartSubject = new BehaviorSubject<Cart>(this.getCartFromStorage());
  public cart$ = this.cartSubject.asObservable();

  constructor() {}

  addToCart(service: Service, quantity: number = 1, notes: string = ''): void {
    const cart = this.cartSubject.value;
    const existingItem = cart.items.find(item => item.service.id === service.id);

    if (existingItem) {
      existingItem.quantity += quantity;
      existingItem.notes = notes;
    } else {
      cart.items.push({ service, quantity, notes });
    }

    this.updateCart(cart);
  }

  removeFromCart(serviceId: string): void {
    const cart = this.cartSubject.value;
    cart.items = cart.items.filter(item => item.service.id !== serviceId);
    this.updateCart(cart);
  }

  updateQuantity(serviceId: string, quantity: number): void {
    const cart = this.cartSubject.value;
    const item = cart.items.find(item => item.service.id === serviceId);

    if (item) {
      item.quantity = quantity;
      if (quantity <= 0) {
        this.removeFromCart(serviceId);
      } else {
        this.updateCart(cart);
      }
    }
  }

  clearCart(): void {
    this.updateCart({ items: [], totalAmount: 0 });
  }

  getCart(): Cart {
    return this.cartSubject.value;
  }

  private updateCart(cart: Cart): void {
    cart.totalAmount = cart.items.reduce(
      (total, item) => total + (item.service.price * item.quantity),
      0
    );

    localStorage.setItem('cart', JSON.stringify(cart));
    this.cartSubject.next(cart);
  }

  private getCartFromStorage(): Cart {
    const cartStr = localStorage.getItem('cart');
    return cartStr ? JSON.parse(cartStr) : { items: [], totalAmount: 0 };
  }
}
