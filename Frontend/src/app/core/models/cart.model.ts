import { Service } from './service.model';

export interface CartItem {
  service: Service;
  quantity: number;
  notes: string;
}

export interface Cart {
  items: CartItem[];
  totalAmount: number;
}
