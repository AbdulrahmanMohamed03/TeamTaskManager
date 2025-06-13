# TeamTaskManager API

A robust Web API for managing projects, tasks, and users with admin-controlled permissions. Built using ASP.NET Core, Entity Framework, and JWT Authentication — following clean architecture principles and Unit of Work pattern.

---

## Features

### Admin Capabilities:
- Register/login users with roles (`admin`, `Member`)
- Add/Edit/Delete **projects**
- Add/Edit/Delete **tasks** within projects
- Assign tasks to users
- Filter projects:
  - Completed
  - Incomplete
  - Late
- View users assigned to specific projects or tasks
- Assign roles to users

### User Capabilities:
- View their own tasks (completed, upcoming, delayed)
- View all projects assigned to them

---

## Tech Stack

- ASP.NET Core Web API (.NET 6+)
- Entity Framework Core
- SQL Server (SSMS)
- JWT Authentication
- Swagger (OpenAPI)
- **Repository Pattern**
- **Unit of Work Pattern**
- Layered Architecture (API / Core / Infrastructure)

---

## Project Structure

```
TeamTaskManager/
├── TeamTaskManager.Api/       → API controllers & config (Presentation Layer)
├── TeamTaskManager.Core/      → Models, DTOs, Interfaces, Services (Business Logic Layer)
├── TeamTaskManager.EF/        → EF Core, Repositories, Unit of Work (Data Access Layer)
```

---

## API Overview

Explore the full API through Swagger:

```
https://localhost:7242/swagger
```

| Category       | Endpoint Examples                             |
|----------------|-----------------------------------------------|
| Authentication | `/api/Authentication/Register`, `/Login`, `/AddRole` |
| Projects       | `/api/Projects/CreateProject`, `/FilterByDate`, `/GetCompletedProjects` |
| Tasks          | `/api/Tasks/AddTaskToProject`, `/GetDelayedTasks`, `/UpdateTask` |
| Users          | `/api/Users/GetById`, `/GetByName`, `/Update`, `/DeleteUser` |

> Full list available via Swagger UI in your browser.

---

## Setup Instructions

1. **Clone the repo:**

```bash
git clone https://github.com/AbdulrahmanMohamed03/TeamTaskManager.git
```

2. **Open in Visual Studio**

3. **Configure `appsettings.json`:**

```json
"ConnectionStrings": {
  "Con" : "Server=.; Database=TeamTaskManager; Integrated Security=True;TrustServerCertificate=True"
},
"JWT": {
  "Key": "BB7wTdogcx5Qik64k+OGgjFjdj7k/qgYETlavTVSQXo=",
  "Issuer": "SecureApi",
  "Audience": "SecureApiUser",
  "DurationInDays": 30  
}
```

4. **Apply EF Migrations:**

```bash
dotnet ef database update
```

> Seed data will create a default admin account automatically.

5. **Run the application:**

```bash
dotnet run
```

6. **Access Swagger:**

```
https://localhost:7242/swagger
```

---

## Architecture Summary

This project uses:
- **Unit of Work** to coordinate data operations across multiple repositories
- **Repository Pattern** to abstract data access logic
- **Clean Separation of Concerns** between API, Business Logic, and Data Access layers

---

## Author

**Abdelrahman Mohamed Talaat**  
🎓 Computer Science Student – Benha University  
🔗 [GitHub](https://github.com/AbdulrahmanMohamed03)  
🔗 [LinkedIn](https://www.linkedin.com/in/abdelrahman-mohamed-8055b4253/)
