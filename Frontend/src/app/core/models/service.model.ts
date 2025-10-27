export interface Service {
  id: string;
  categoryId: string;
  categoryName: string;
  name: string;
  description: string;
  type: ServiceType;
  price: number;
  unit: string;
  durationMinutes: number;
  imageUrl: string;
}

export interface ServiceCategory {
  id: string;
  name: string;
  description: string;
  iconUrl: string;
  services: Service[];
}

export enum ServiceType {
  Hourly = 0,
  Package = 1,
  PerArea = 2
}
