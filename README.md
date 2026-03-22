# 📚 Library Management System — Backend API

A scalable and production-ready backend API for a Library Management System, built with modern .NET technologies following industry best practices.

---

## 🛠️ Tech Stack

| Technology | Purpose |
|---|---|
| ⚡ .NET 8 | Backend framework |
| 🏗️ Clean Architecture | Project structure |
| 🔄 CQRS + MediatR | Command/Query separation |
| 🗺️ AutoMapper | Object mapping |
| 🗄️ Entity Framework Core | ORM |
| 🐘 PostgreSQL (Supabase) | Cloud database |
| 🔐 JWT Bearer | Authentication |
| ✅ FluentValidation | Input validation |
| 📝 Swagger / OpenAPI | API documentation |

---

## 🏛️ Architecture

This project follows **Clean Architecture** with 4 layers:

```
LibraryManagementSystem/
│
├── 🌐 API              → Controllers, Middleware, Program.cs
├── 💼 Application      → CQRS, Handlers, DTOs, Mappings
├── 🧠 Core             → Entities, Interfaces (no dependencies)
└── ⚙️ Infrastructure   → DB, Repositories, Services
```

### Dependency Flow
```
API → Application → Core
           ↓
     Infrastructure
```

---

## 🔄 CQRS Pattern

```
Features/
  └── Users/
       ├── Commands/    → write operations
       ├── Queries/     → read operations
       ├── Handlers/    → business logic
       └── DTOs/        → request & response models
```

---

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- PostgreSQL or Supabase account

### Run Locally

```bash
# clone the repo
git clone https://github.com/prasadm11/LibraryManagementSystemBE.git

# navigate to project
cd LibraryManagementSystemBE

# run migrations
dotnet ef database update -p LibraryManagementSystem.Infrastructure -s LibraryManagementSystem.API

# run the project
dotnet run --project LibraryManagementSystem.API
```

### Open Swagger
```
https://localhost:7172/swagger
```

---

## ⚙️ Configuration

Update `appsettings.json` with your credentials:

```json
"ConnectionStrings": {
  "PostgreSQLConnection": "your_supabase_connection_string"
},
"Jwt": {
  "Key": "your_secret_key",
  "Issuer": "LibraryManagementSystem",
  "Audience": "LibraryManagementSystemUsers",
  "ExpiryHours": "2"
}
```

---

## 🗺️ Roadmap

- [x] 👤 User management
- [x] 🔐 JWT Authentication
- [ ] 📖 Book management
- [ ] 📋 Borrow & return records
- [ ] 🛡️ Role-based authorization
- [ ] 🔒 Password hashing
- [ ] 🔄 Refresh tokens
- [ ] 🚨 Global exception handling

---

## 👨‍💻 Author

**Prasad Mahajan**  
[GitHub](https://github.com/prasadm11) · [LinkedIn](https://linkedin.com/in/prasadmahajan21)

---

## 📄 License

This project is licensed under the MIT License.
