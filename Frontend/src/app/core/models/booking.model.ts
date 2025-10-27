export interface Booking {
  id: string;
  bookingNumber: string;
  scheduledDate: Date;
  scheduledTime: string;
  address: string;
  city: string;
  status: BookingStatus;
  totalAmount: number;
  items: BookingItem[];
  assignedStaff?: StaffInfo;
  createdAt: Date;
}

export interface BookingItem {
  id: string;
  serviceName: string;
  quantity: number;
  unitPrice: number;
  totalPrice: number;
}

export interface CreateBookingRequest {
  items: BookingItemRequest[];
  scheduledDate: Date;
  scheduledTime: string;
  address: string;
  city: string;
  district: string;
  postalCode: string;
  notes: string;
  preferredStaffId?: string;
}

export interface BookingItemRequest {
  serviceId: string;
  quantity: number;
  notes: string;
}

export interface StaffInfo {
  id: string;
  firstName: string;
  lastName: string;
  rating: number;
  completedJobs: number;
}

export enum BookingStatus {
  Pending = 0,
  Confirmed = 1,
  Assigned = 2,
  InProgress = 3,
  Completed = 4,
  Cancelled = 5
}
