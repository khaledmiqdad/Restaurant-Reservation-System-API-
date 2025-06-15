# 🍽️ Restaurant Reservation System API

![.NET](https://img.shields.io/badge/.NET-8-512BD4?logo=dotnet)
![EF Core](https://img.shields.io/badge/EF_Core-7.0-green)
![JWT Auth](https://img.shields.io/badge/JWT-Auth-orange)
![Swagger](https://img.shields.io/badge/Swagger-Docs-85EA2D)

A comprehensive restaurant reservation management system API built with ASP.NET Core Web API and Entity Framework Core.

## 🌟 Features

- **Full CRUD operations** for all entities (Restaurants, Reservations, Orders, etc.)
- **JWT Authentication** for secure endpoints
- **Comprehensive documentation** with Swagger
- **Advanced querying** with:
  - Database views
  - Stored procedures
  - Custom functions
- **Validation** and error handling
- **Postman test collection** for easy testing

## 🏗️ Project Structure

```
Restaurant-Reservation/
├── RestaurantReservation.API/        # Web API project
├── RestaurantReservation.Db/         # Data access layer
│   ├── Models/                       # Entity models
│   ├── Repositories/                 # Repository classes
│   ├── Migrations/                   # Database migrations
│   └── RestaurantReservationDbContext.cs
└── RestaurantReservation.Domain/            
```

## 🔌 API Endpoints

### 📍 Restaurants
- `GET /api/restaurants` - List all restaurants
- `GET /api/restaurants/{id}` - Get restaurant details by ID
- `POST /api/restaurants` - Create a new restaurant
- `PUT /api/restaurants/{id}` - Update an existing restaurant (full update)
- `PATCH /api/restaurants/{id}` - Partially update an existing restaurant
- `DELETE /api/restaurants/{id}` - Delete a restaurant by ID

### 📅 Reservations
- `GET /api/reservations` - List all reservations
- `GET /api/reservations/{id}` - Get reservation details by reservation ID
- `GET /api/reservations/customer/{customerId}` - Get all reservations for a specific customer
- `GET /api/reservations/{id}/orders` - Get all orders linked to a specific reservation
- `GET /api/reservations/{id}/menu-items` - Get all menu items associated with a specific reservation
- `POST /api/reservations` - Create a new reservation
- `PUT /api/reservations/{id}` - Update an existing reservation (full update)
- `PATCH /api/reservations/{id}` - Partially update an existing reservation
- `DELETE /api/reservations/{id}` - Delete a reservation by ID


### 👨‍🍳 Employees
- `GET /api/employees` - List all employees
- `GET /api/employees/{id}` - Get employee details by employee ID
- `GET /api/employees/{id}/orders` - Get all orders handled by a specific employee
- `GET /api/employees/{id}/restaurant` - Get the restaurant details where the employee works
- `GET /api/employees?managersOnly=true` - List only employees who are managers
- `GET /api/employees/{id}/order/average-amount` - Get the average amount of orders handled by the employee
- `POST /api/employees` - Create a new employee
- `PUT /api/employees/{id}` - Update an existing employee (full update)
- `PATCH /api/employees/{id}` - Partially update an existing employee
- `DELETE /api/employees/{id}` - Delete an employee by ID


### 🍽️ Menu Items
- `GET /api/menu-items` - List all menu items
- `GET /api/menu-items/{id}` - Get details of a specific menu item by ID
- `GET /api/menu-items/{id}/restaurant` - Get the restaurant that offers a specific menu item
- `POST /api/menu-items` - Create a new menu item
- `PUT /api/menu-items/{id}` - Update an existing menu item (full update)
- `PATCH /api/menu-items/{id}` - Partially update an existing menu item
- `DELETE /api/menu-items/{id}` - Delete a menu item by ID


### 🔐 Authentication
- `POST /api/auth/login` - Get JWT token

### **Note:**
The same API endpoint principles outlined above (create, read, full and partial update, and delete) apply to all entities. You can assume standard CRUD endpoints for each entity, using `/api/{entity-name}` as the base. Additionally, a **Postman collection** is available in the project files, containing all endpoints, including both success and failure cases.

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server
- Postman (for testing)

### Running the API
1. Clone the repository
2. Update connection string in `appsettings.json`
3. Run migrations:
   ```bash
   dotnet ef database update
   ```
4. Start the API:
   ```bash
   dotnet run
   ```

## 📚 API Documentation

Swagger documentation is available at `/swagger` when running the API.

## 🧪 Testing

Import the provided Postman collection to test all endpoints.

Sample test cases:
- Create reservation
- Get manager list
- Calculate average order amount
- JWT authentication flow

## 📊 Database Schema

The database includes tables for:
- Restaurants
- Reservations
- Orders
- MenuItems
- Employees
- Customers
- Tables
- OrderItems
