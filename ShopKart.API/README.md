## 📄 README.md:

```markdown
# 🛒 ShopKart API — E-Commerce Backend

A professional-grade E-Commerce Backend REST API built with modern .NET technologies. This project demonstrates clean architecture, design patterns, and industry best practices.

---

## 🚀 Tech Stack

| Technology | Version |
|-----------|---------|
| ASP.NET Core | 10 |
| Entity Framework Core | 10 |
| SQL Server | LocalDB 2022 |
| xUnit | Latest |
| Moq | Latest |

---

## 🏗️ Architecture & Design Patterns

```
ShopKart.API/
├── Controllers/          → API Endpoints (HTTP handling)
├── Services/             → Business Logic Layer
├── Repositories/         → Data Access Layer
├── Models/Entities/      → Domain Models
├── DTOs/                 → Data Transfer Objects
├── Strategies/           → Strategy Pattern (Payments)
├── Middleware/            → Global Exception Handling
└── Data/                 → EF Core DbContext + Configuration
```

### Design Patterns Implemented:
| Pattern | Usage |
|---------|-------|
| **Repository Pattern** | Generic + Specific repositories for data access abstraction |
| **Unit of Work** | Transaction management across multiple repositories |
| **Strategy Pattern** | Payment processing (Credit Card, UPI, COD) |
| **DTO Pattern** | Separate Create, Update, and Response DTOs with validation |
| **Dependency Injection** | Loosely coupled architecture throughout the app |

### SOLID Principles:
| Principle | Implementation |
|-----------|---------------|
| **Single Responsibility** | Controller → Service → Repository separation |
| **Open/Closed** | New payment methods without modifying existing code |
| **Liskov Substitution** | All payment strategies interchangeable via IPaymentStrategy |
| **Interface Segregation** | IGenericRepository vs IProductRepository vs ICategoryRepository |
| **Dependency Inversion** | Every layer depends on abstractions, not concrete classes |

---

## 📦 API Endpoints

### 📁 Products
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Product` | Get all products |
| GET | `/api/Product/{id}` | Get product by ID |
| GET | `/api/Product/category/{categoryId}` | Get products by category |
| POST | `/api/Product` | Create a new product |
| PUT | `/api/Product/{id}` | Update a product |
| DELETE | `/api/Product/{id}` | Soft delete a product |

### 📁 Categories
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Category` | Get all categories |
| GET | `/api/Category/{id}` | Get category by ID |
| GET | `/api/Category/{id}/products` | Get category with its products |
| POST | `/api/Category` | Create a new category |
| PUT | `/api/Category/{id}` | Update a category |
| DELETE | `/api/Category/{id}` | Soft delete a category |

### 💳 Payments (Strategy Pattern)
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/Payment/process` | Process payment (Credit Card / UPI / COD) |

---

## 🛡️ Features

- ✅ **Layered Architecture** — Controller → Service → Repository
- ✅ **Generic Repository** — Reusable CRUD operations for any entity
- ✅ **Unit of Work** — Single transaction across multiple repositories
- ✅ **Strategy Pattern** — Extensible payment processing system
- ✅ **DTO Validation** — Data Annotations (Layer 1) + Service Validation (Layer 2)
- ✅ **Global Exception Handling** — Centralized error responses via middleware
- ✅ **Soft Delete** — Records marked inactive, never physically deleted
- ✅ **Seed Data** — Pre-populated Categories, Products, and Customers
- ✅ **Fluent API** — Entity configurations with proper constraints
- ✅ **Unit Testing** — xUnit + Moq for service layer testing

---

## 🧪 Testing

Unit tests written using **xUnit** and **Moq** for mocking dependencies.

```bash
# Run all tests
dotnet test
```

### Test Coverage:
| Test | Scenario |
|------|----------|
| GetAllProducts | Returns product list with category names |
| GetProductById | Returns null when product not found |
| CreateProduct | Returns DTO with valid data |
| CreateProduct | Throws KeyNotFoundException for invalid CategoryId |
| DeleteProduct | Returns true when product exists |
| DeleteProduct | Returns false when product not found |

---

## ⚙️ Setup & Run

### Prerequisites:
- .NET 10 SDK
- SQL Server LocalDB 2022
- Visual Studio 2026 / VS Code

### Steps:

```bash
# 1. Clone the repository
git clone https://github.com/Zishan-ansarii/ShopKart.API.git

# 2. Navigate to project
cd ShopKart.API

# 3. Restore packages
dotnet restore

# 4. Apply migrations
dotnet ef database update

# 5. Run the application
dotnet run

# 6. Open Swagger
# Navigate to: https://localhost:7000/swagger
```

---

## 📊 Database Schema

```
Categories ──────┐
    │             │
    │ 1:Many      │
    ▼             │
Products ────────┤
    │             │
    │ 1:Many      │
    ▼             │
OrderItems ◄─────┤
    │             │
    │ Many:1      │
    ▼             │
Orders ──────────┤
    │             │
    │ Many:1      │
    ▼             │
Customers ───────┘
```

---

## 💳 Payment Strategy Example

```json
// POST /api/Payment/process

// UPI Payment
{
  "paymentMethod": "UPI",
  "amount": 999.00
}

// Credit Card Payment (minimum ₹100)
{
  "paymentMethod": "CreditCard",
  "amount": 5000.00
}

// Cash on Delivery (maximum ₹5000)
{
  "paymentMethod": "COD",
  "amount": 2500.00
}
```

---

## 📌 Key Learnings

This project was built as a learning exercise covering:
- Clean Architecture in ASP.NET Core
- Design Patterns (Repository, Unit of Work, Strategy)
- SOLID Principles with practical implementation
- Unit Testing with xUnit and Moq
- REST API best practices (proper HTTP status codes, DTOs, validation)
- Entity Framework Core (Fluent API, Migrations, Seed Data)

---

## 📝 License

This project is for educational purposes.

---

## 🤝 Author

**Zishan Ansari**
- Aspiring .NET Developer
- Focused on building production-ready backend systems
```

---
