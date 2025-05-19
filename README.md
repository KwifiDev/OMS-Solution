# OMS Solution

A robust, modular, and scalable Office Management System (OMS) designed using best practices in software architecture with C# and .NET.  
This project is engineered for maintainability, extensibility, and seamless integration across modern enterprise environments.

---

## Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Project Structure](#project-structure)
- [Key Features](#key-features)
- [Technology Stack](#technology-stack)
- [Installation & Setup](#installation--setup)
- [Usage](#usage)
- [API Documentation](#api-documentation)
- [Contribution Guidelines](#contribution-guidelines)
- [License](#license)
- [Contact](#contact)

---

## Overview

**OMS Solution** is a comprehensive platform for managing office operations, employees, attendance, and tasks. The system is built on a clean separation of concerns, ensuring each layer is independently testable, replaceable, and upgradable.

---

## Architecture

The project adheres to a layered architecture pattern, promoting clear separation between data access, business logic, API, and user interface:

```
[OMS.UI] <--> [OMS.API] <--> [OMS.BL] <--> [OMS.DA]
```

- **OMS.DA (Data Access Layer):**  
  Encapsulates all database operations, repositories, and data models.  
  Implements patterns like Repository and Unit of Work for consistency and testability.

- **OMS.BL (Business Logic Layer):**  
  Contains core business rules, domain services, validation, and orchestration logic.  
  Ensures the integrity and correctness of all operations.

- **OMS.API (API Layer):**  
  ASP.NET Core RESTful API adhering to best practices and standards (REST, versioning, OpenAPI).  
  Serves as the sole entry point for all client applications, providing secure and well-documented endpoints.

- **OMS.UI (User Interface Layer):**  
  Decoupled client application (e.g., WPF MVVM, Web, or Mobile) interacting exclusively with OMS.API.  
  Designed for enhanced user experience and ease of integration with the backend.

---

## Project Structure

```
OMS-Solution/
├── OMS.DA/    # Data Access Layer
├── OMS.BL/    # Business Logic Layer
├── OMS.API/   # API Layer (RESTful, ASP.NET Core)
└── OMS.UI/    # User Interface Layer (e.g., WPF MVVM)
```

---

## Key Features

- **Employee Management:**  
  Add, update, delete, and track employee records.

- **Attendance Tracking:**  
  Record, monitor, and report employee attendance.

- **Task Management:**  
  Assign, track, and manage office tasks with full audit trail.

- **Role-Based Access Control:**  
  Multi-level authentication and authorization with secure token management.

- **Health Monitoring:**  
  Built-in health checks and endpoints for API monitoring and automated DevOps.

- **Extensible API:**  
  Modern RESTful API, documented with Swagger/OpenAPI, ready for integration with any modern frontend or third-party system.

- **Separation of Concerns:**  
  Each layer can be tested and scaled independently, supporting CI/CD and microservices adoption.

---

## Technology Stack

- **.NET 8 / ASP.NET Core** (OMS.API, OMS.BL, OMS.DA)
- **Entity Framework Core** (Data Access)
- **WPF (MVVM)** or other front-end frameworks (OMS.UI)
- **Swagger/OpenAPI** (API Documentation)
- **JWT Authentication**
- **SQL Server** (default, can be replaced)
- **xUnit / NUnit** (Testing)
- **Docker** (optional, for deployment)

---

## Installation & Setup

1. **Clone the repository:**
    ```bash
    git clone https://github.com/KwifiDev/OMS-Solution.git
    ```

2. **Configure the Database:**
    - Update connection strings in `OMS.API/appsettings.json` and/or `OMS.DA/config`.
    - Apply migrations (if using EF Core):
      ```bash
      dotnet ef database update --project OMS.DA
      ```

3. **Build the Solution:**
    - Open in Visual Studio 2022+ or use CLI:
      ```bash
      dotnet build OMS-Solution.sln
      ```

4. **Run the API:**
    - Set OMS.API as the startup project.
    - Launch via Visual Studio or:
      ```bash
      dotnet run --project OMS.API
      ```
    - API will be available at `https://localhost:5001` (default).

5. **Run the UI:**
    - Set OMS.UI as the startup project.
    - Configure API base URL in OMS.UI settings.

---

## Usage

- **Login:**  
  Authenticate using your credentials via the UI.

- **Modules:**  
  - Employees: Manage staff information.
  - Attendance: Record/check attendance logs.
  - Tasks: Assign/manage office tasks.
  - Reports: Generate operational reports.

- **API:**  
  Use Swagger (`/swagger`) for live API testing and documentation.

---

## API Documentation

- After launching OMS.API, navigate to:  
  `https://localhost:5001/swagger`
- Explore and test all available endpoints.

---

## Contribution Guidelines

1. Fork the repository.
2. Create a feature branch:
    ```bash
    git checkout -b feature/your-feature
    ```
3. Commit your changes with clear messages.
4. Push to your fork and submit a Pull Request.
5. Ensure your code passes all tests and adheres to project conventions.

---

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE) for details.

---

## Contact

- **Author:** Mohamad Munir Alkwifi  
- **GitHub:** [KwifiDev](https://github.com/KwifiDev)  
- **Issues & Feedback:** [GitHub Issues](https://github.com/KwifiDev/OMS-Solution/issues)

---

**For more details, visit the full [commit history](https://github.com/KwifiDev/OMS-Solution/commits/main).**