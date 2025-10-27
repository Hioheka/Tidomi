# Tidomi - Cleaning Service Platform

A full-stack web application for booking professional cleaning services built with .NET Core 8, Angular 20, PostgreSQL, and Swagger.

## 🚀 Features

### Customer Features
- User Authentication with JWT
- Browse services by categories (Home, Office, Deep Cleaning, Window Cleaning)
- Shopping cart functionality
- Book cleaning appointments with date/time selection
- Multi-language support (English & Turkish)
- Track booking status

### Staff Features
- View assigned bookings
- Update booking status
- Manage availability

### Admin Features
- View all bookings
- Assign staff to bookings
- Manage services and categories

## 📋 Tech Stack

**Backend:**
- .NET Core 8 Web API
- Entity Framework Core 8
- PostgreSQL
- JWT Authentication
- Swagger/OpenAPI
- Clean Architecture

**Frontend:**
- Angular 20
- TypeScript
- RxJS
- ngx-translate (i18n)
- SCSS

## 🛠️ Installation

### Prerequisites
- .NET 8 SDK
- Node.js 18+
- PostgreSQL 12+

### Backend Setup

```bash
cd Backend
dotnet restore
dotnet run --project Tidomi.API
```

**API will run on:** `http://localhost:5000`
**Swagger UI:** `http://localhost:5000/swagger`

**Database:** Connection string in `appsettings.json`. Database auto-migrates on first run.

### Frontend Setup

```bash
cd Frontend
npm install
npm start
```

**App will run on:** `http://localhost:4200`

## 📚 API Endpoints

### Authentication
```
POST /api/auth/register - Register
POST /api/auth/login    - Login
```

### Services
```
GET /api/services/categories            - Get all categories
GET /api/services/category/{categoryId} - Get services by category
GET /api/services/{id}                  - Get service details
```

### Bookings (Authenticated)
```
POST /api/bookings              - Create booking
GET  /api/bookings/my-bookings  - Get user bookings
GET  /api/bookings/{id}         - Get booking details
GET  /api/bookings/all          - Get all bookings (Admin/Staff)
```

## 🗂️ Project Structure

```
Tidomi/
├── Backend/
│   ├── Tidomi.API/              # Controllers & API Configuration
│   ├── Tidomi.Application/      # DTOs & Interfaces
│   ├── Tidomi.Domain/           # Entities & Models
│   └── Tidomi.Infrastructure/   # DbContext & Services
├── Frontend/
│   └── src/
│       ├── app/
│       │   ├── core/            # Services, Guards, Models
│       │   └── features/        # Components (Auth, Cart, Booking, etc.)
│       └── assets/i18n/         # Translation files (en.json, tr.json)
└── README.md
```

## 🔐 Authentication

JWT tokens are issued on login/register. Include in requests:
```
Authorization: Bearer {token}
```

**Roles:** Customer, Staff, Admin

## 🌍 Multi-language Support

Supports English and Turkish. Translation files:
- `Frontend/src/assets/i18n/en.json`
- `Frontend/src/assets/i18n/tr.json`

## 💳 Payment (TODO)

Payment page is a placeholder. Future integrations:
- iyzico (Turkish payment gateway)
- PayTR (Turkish payment gateway)
- Stripe (International)

## 🎯 Usage Flow

1. **Register/Login** → Create account or sign in
2. **Browse Services** → View cleaning services by category
3. **Add to Cart** → Select services and quantities
4. **Checkout** → Review cart and proceed
5. **Create Booking** → Enter date, time, and address
6. **Payment** → (TODO) Complete payment
7. **Track Booking** → View booking status

## 🧪 Testing

**Swagger UI:** `http://localhost:5000/swagger`

1. Register a user via `/api/auth/register`
2. Login via `/api/auth/login` (copy JWT token)
3. Click "Authorize" in Swagger, enter: `Bearer {token}`
4. Test authenticated endpoints

## 📦 Seeded Data

Initial database includes:
- 4 Service Categories
- 5 Sample Services
- Various pricing models (Hourly, Package, Per-Area)

## 🚀 Deployment

**Backend:**
```bash
dotnet publish -c Release
```

**Frontend:**
```bash
npm run build
```

Deploy `dist/` folder to your hosting service.

## 🐛 Troubleshooting

**Database Connection:** Update `appsettings.json` with your PostgreSQL credentials
**CORS Issues:** Backend configured for `localhost:4200`
**Port Conflicts:** Change ports in `launchSettings.json` or `angular.json`

## 📝 License

MIT License

## 👨‍💻 Development

**Backend (with hot reload):**
```bash
dotnet watch run --project Backend/Tidomi.API
```

**Frontend (with hot reload):**
```bash
cd Frontend && npm start
```

---

**Note:** This is a development build. Payment integration and additional features are planned for future releases.
