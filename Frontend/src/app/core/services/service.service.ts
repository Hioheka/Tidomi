import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Service, ServiceCategory } from '../models/service.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ServiceService {
  constructor(private http: HttpClient) {}

  getAllCategories(language: string = 'en'): Observable<ServiceCategory[]> {
    return this.http.get<ServiceCategory[]>(
      `${environment.apiUrl}/services/categories?language=${language}`
    );
  }

  getServicesByCategory(categoryId: string, language: string = 'en'): Observable<Service[]> {
    return this.http.get<Service[]>(
      `${environment.apiUrl}/services/category/${categoryId}?language=${language}`
    );
  }

  getServiceById(id: string, language: string = 'en'): Observable<Service> {
    return this.http.get<Service>(
      `${environment.apiUrl}/services/${id}?language=${language}`
    );
  }
}
