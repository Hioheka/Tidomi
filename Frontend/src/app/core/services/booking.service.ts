import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Booking, CreateBookingRequest } from '../models/booking.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  constructor(private http: HttpClient) {}

  createBooking(booking: CreateBookingRequest): Observable<Booking> {
    return this.http.post<Booking>(`${environment.apiUrl}/bookings`, booking);
  }

  getMyBookings(): Observable<Booking[]> {
    return this.http.get<Booking[]>(`${environment.apiUrl}/bookings/my-bookings`);
  }

  getBookingById(id: string): Observable<Booking> {
    return this.http.get<Booking>(`${environment.apiUrl}/bookings/${id}`);
  }

  getAllBookings(): Observable<Booking[]> {
    return this.http.get<Booking[]>(`${environment.apiUrl}/bookings/all`);
  }

  getAvailableDates(staffId?: string): Observable<Date[]> {
    const url = staffId
      ? `${environment.apiUrl}/bookings/available-dates?staffId=${staffId}`
      : `${environment.apiUrl}/bookings/available-dates`;
    return this.http.get<Date[]>(url);
  }

  assignStaff(bookingId: string, staffId: string): Observable<void> {
    return this.http.put<void>(
      `${environment.apiUrl}/bookings/${bookingId}/assign-staff`,
      { staffId }
    );
  }

  updateStatus(bookingId: string, status: number): Observable<void> {
    return this.http.put<void>(
      `${environment.apiUrl}/bookings/${bookingId}/status`,
      { status }
    );
  }
}
