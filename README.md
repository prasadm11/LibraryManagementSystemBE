# 📚 Library Management System API

A robust ASP.NET Core Web API for managing library operations including books, borrowing, reservations, ratings, notifications, and user management.

---

## 🚀 Features

- User Authentication & Authorization
- Book Management (CRUD)
- Borrow & Return Books
- Book Reservations
- Book Ratings & Reviews
- Borrow Request Approval Workflow
- Notification Management
- Fine Payment Support
- Borrow History Tracking
- Due Soon & Overdue Book Monitoring

---

## 🛠️ Technology Stack

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- MediatR
- JWT Authentication
- AutoMapper
- FluentValidation

---

## 📂 API Modules

### Authentication

| Method | Endpoint | Description |
|----------|------------|-------------|
| POST | /api/Auth/LoginUser | User Login |

---

### Book Rating

| Method | Endpoint | Description |
|----------|------------|-------------|
| POST | /api/BookRating/RateBook | Rate a Book |
| GET | /api/BookRating/GetBookRatings | Get Book Ratings |

---

### Book Reservation

| Method | Endpoint | Description |
|----------|------------|-------------|
| POST | /api/BookReservation/CreateReservation | Create Reservation |
| POST | /api/BookReservation/CancelReservation | Cancel Reservation |
| GET | /api/BookReservation/GetBookReservations | Get All Reservations |
| GET | /api/BookReservation/GetUserReservations | Get User Reservations |

---

### Books

| Method | Endpoint | Description |
|----------|------------|-------------|
| GET | /api/Books/GetAllBooks | Get All Books |
| GET | /api/Books/GetBookById/{id} | Get Book By Id |
| POST | /api/Books/AddBook | Add New Book |
| PUT | /api/Books/UpdateBook | Update Book |
| DELETE | /api/Books/DeleteBook/{id} | Delete Book |

---

### Borrow Management

| Method | Endpoint | Description |
|----------|------------|-------------|
| POST | /api/Borrow/BorrowBook | Borrow Book |
| POST | /api/Borrow/ReturnBook | Return Book |
| POST | /api/Borrow/RenewBook | Renew Borrowed Book |
| POST | /api/Borrow/PayFine | Pay Borrow Fine |
| GET | /api/Borrow/GetBooksByStatus | Get Books By Status |
| GET | /api/Borrow/GetUserBorrowHistory | Get User Borrow History |
| GET | /api/Borrow/GetOverdueBooks | Get Overdue Books |
| GET | /api/Borrow/GetDueBookSoon | Get Due Soon Books |
| GET | /api/Borrow/SearchBooks | Search Books |
| GET | /api/Borrow/GetBorrowSummary | Borrow Statistics |
| GET | /api/Borrow/CheckBorrowEligibility | Check Borrow Eligibility |

---

### Borrow Requests

| Method | Endpoint | Description |
|----------|------------|-------------|
| POST | /api/BorrowRequest/CreateBorrowRequest | Create Borrow Request |
| GET | /api/BorrowRequest/GetAllPendingBorrowRequests | Get Pending Requests |
| POST | /api/BorrowRequest/ApproveRequest | Approve Request |
| POST | /api/BorrowRequest/RejectRequest | Reject Request |
| POST | /api/BorrowRequest/CreateReturnBookRequest | Create Return Request |
| POST | /api/BorrowRequest/CreateRenewBookRequest | Create Renew Request |

---

### Notifications

| Method | Endpoint | Description |
|----------|------------|-------------|
| GET | /api/Notification/GetNotificationsByUserId | Get User Notifications |
| POST | /api/Notification/MarkAsRead | Mark Notification As Read |

---

### User Management

| Method | Endpoint | Description |
|----------|------------|-------------|
| GET | /api/User/GetAllUsers | Get All Users |
| GET | /api/User/GetUserById/{id} | Get User By Id |
| POST | /api/User/CreateUser | Create User |
| PUT | /api/User/UpdateUser | Update User |
| DELETE | /api/User/DeleteUser/{id} | Delete User |

---

## 🔐 Authentication

The API uses JWT Bearer Authentication.

Add the token to the request header:

http Authorization: Bearer <your-jwt-token> 

---

## ⚙️ Running Locally

### Prerequisites

- .NET 8 SDK 
- SQL Server
- Visual Studio 2022 / VS Code

### Clone Repository

bash git clone https://github.com/prasadm11/LibraryManagementSystemBE.git

### Configure Database

Update appsettings.json:

json {   "ConnectionStrings": {     "DefaultConnection": "Your_SQL_Server_Connection_String"   } } 

### Apply Migrations

bash dotnet ef database update 

### Run Application

bash dotnet run 

Swagger UI: 

text https://localhost:<port>/swagger 

---

## 📊 Business Rules

### Borrowing

- Users can borrow available books.
- Borrow eligibility is validated before issuing.
- Borrowed books can be renewed.
- Overdue books may incur fines.

### Reservations

- Users can reserve unavailable books.
- Reservations can be cancelled.
- Reservation history is maintained.

### Ratings

- Users can rate books.
- Ratings are aggregated and available through the API.

### Notifications

- System-generated notifications inform users about:
  - Due books
  - Overdue books
  - Approved requests
  - Rejected requests
  - Reservation updates

---

## 📖 Swagger Documentation

After running the application, access:

text https://localhost:<port>/swagger 

to explore and test all available endpoints.

---

## 👨‍💻 Author

Prasad Mahajan

---

## 📄 License

This project is licensed under the MIT Licen
