import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { ServiceService } from '../../core/services/service.service';
import { CartService } from '../../core/services/cart.service';
import { ServiceCategory } from '../../core/models/service.model';

@Component({
  selector: 'app-services',
  standalone: true,
  imports: [CommonModule, TranslateModule],
  template: `
    <div class="services-container">
      <h2>{{ 'services.title' | translate }}</h2>

      <div *ngFor="let category of categories" class="category">
        <h3>{{ category.name }}</h3>
        <p>{{ category.description }}</p>

        <div class="services-grid">
          <div *ngFor="let service of category.services" class="service-card">
            <h4>{{ service.name }}</h4>
            <p>{{ service.description }}</p>
            <div class="service-info">
              <span>{{ 'services.price' | translate }}: {{ service.price }} {{ service.unit }}</span>
              <span>{{ 'services.duration' | translate }}: {{ service.durationMinutes }} {{ 'services.minutes' | translate }}</span>
            </div>
            <button (click)="addToCart(service)">{{ 'services.addToCart' | translate }}</button>
          </div>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .services-container { padding: 2rem; }
    .category { margin-bottom: 2rem; }
    .services-grid { display: grid; grid-template-columns: repeat(auto-fill, minmax(300px, 1fr)); gap: 1.5rem; margin-top: 1rem; }
    .service-card { border: 1px solid #ddd; border-radius: 8px; padding: 1.5rem; }
    .service-info { display: flex; flex-direction: column; gap: 0.5rem; margin: 1rem 0; font-size: 0.9rem; color: #666; }
    button { width: 100%; padding: 0.75rem; background: #28a745; color: white; border: none; border-radius: 4px; cursor: pointer; }
  `]
})
export class ServicesComponent implements OnInit {
  categories: ServiceCategory[] = [];

  constructor(
    private serviceService: ServiceService,
    private cartService: CartService,
    private translate: TranslateService
  ) {}

  ngOnInit() {
    const language = this.translate.currentLang || 'en';
    this.serviceService.getAllCategories(language).subscribe(
      data => this.categories = data
    );
  }

  addToCart(service: any) {
    this.cartService.addToCart(service, 1);
    alert('Added to cart!');
  }
}
