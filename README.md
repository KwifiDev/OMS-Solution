# Office Management System (OMS)

This project is a full-stack Office Management System built with **.NET 9**, following clean architecture principles and modern design patterns.

## Overview

**OMS Solution** is a comprehensive platform for managing office operations, employees, attendance, and tasks. The system is built on a clean separation of concerns, ensuring each layer is independently testable, replaceable, and upgradable.

---

## 📌 Project Structure

- **OMS.UI**  
  - WPF Client Application  
  - Implements **MVVM (Model-View-ViewModel)** pattern  
  - Provides a user-friendly interface for office management tasks

- **OMS.API**  
  - ASP.NET Core Web API  
  - Serves as the backend for business operations and communication with the database

- **OMS.BL (Business Layer)**  
  - Contains core business logic  
  - Implements service classes, validations, and rules

- **OMS.DA (Data Access Layer)**  
  - Responsible for database operations  
  - Implements Repository pattern for separation of concerns

- **OMS.Common**  
  - Shared models, DTOs, and utilities used across different layers

## ⚙️ Architecture

The solution follows a **Three-Tier Architecture** combined with clean coding practices:

1. **Presentation Layer** → OMS.UI (WPF MVVM)  
2. **Application & Business Layer** → OMS.API + OMS.BL  
3. **Data Layer** → OMS.DA  

Communication between components is handled via **Web API (JSON over HTTP)**.

## 🛠️ Technologies Used

- **.NET 9**  
- **WPF (MVVM)**  
- **ASP.NET Core Web API**  
- **Entity Framework Core**  
- **SQL Server (LocalDB)**  

## 🚀 Features

- Modular and extensible design  
- Separation of concerns with layered architecture  
- Clean code principles applied  
- Shared project for common models and utilities  
- Designed for scalability and maintainability  

## 📖 How to Run

1. Clone the repository  
   ```bash
   git clone <repository-url>
   ```

2. Open the solution in **Visual Studio 2022+**  

3. Build the solution to restore dependencies  

4. Start the **OMS.API** project to run the backend  

5. Run the **OMS.UI (WPF)** project to launch the desktop application  

## 📂 Folder Structure

```
OMS-Solution
│
├── OMS.UI          # WPF MVVM Client
├── OMS.API         # ASP.NET Core Web API
├── OMS.BL          # Business Logic Layer
├── OMS.DA          # Data Access Layer
├── OMS.Common      # Shared Models & Utilities
```

## 📌 Notes

- The project was developed as a **graduation-level project** by a developer specialized in Software & Information Systems.  
- While not yet industry-level, it demonstrates solid understanding of architectural patterns and clean design principles.  

---

## Contact

- **Author:** Mohamad Munir Alkwifi  
- **GitHub:** [KwifiDev](https://github.com/KwifiDev)  
- **Issues & Feedback:** [GitHub Issues](https://github.com/KwifiDev/OMS-Solution/issues)

---

**For more details, visit the full [commit history](https://github.com/KwifiDev/OMS-Solution/commits/main).**

👨‍💻 **Author:** Mohamad Munir Alkwifi  
