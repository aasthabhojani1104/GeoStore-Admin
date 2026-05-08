# 🌍 GeoStore Admin Panel (Country_Store)

> A powerful, role-based **Admin Management System** built with **ASP.NET MVC** to manage Countries, States, Cities, Stores, and Users with smart permission control.

---

## ✨ Why this project stands out

* 🔐 Secure authentication system
* 🧠 Smart **Role + Permission based access**
* ⚡ Smooth **AJAX-powered UI (no reloads)**
* 📍 Real-time **Google Maps integration for store locations**
* 📊 Clean, modern, dashboard-style UI
* 🏗️ Proper layered architecture (Controller → Service → DAL)

---

## 🚀 Features

### 🔑 Authentication & Security

* Login / Logout system
* Session-based authentication
* Custom **PermissionAuthorize Attribute**

### 👤 Role & Permission System

* **Admin:** Full system control
* **User:** Limited access based on permissions
* Admin panel is **completely hidden** for normal users ❌

### 🌍 Country / State / City Management

* Full CRUD operations
* Search functionality
* Pagination support
* Dynamic dropdowns (Country → State → City)

### 🏪 Store Management

* Add stores with:

  * Opening / Closing time
  * Address
  * Location mapping
* 📍 One-click **Google Maps navigation**

### 👥 User Management

* Create users
* Assign roles (Admin / User)
* Enable/Disable users
* Assign module-level permissions

### 🔐 Permission Management

* Assign permissions dynamically:

  * Country
  * State
  * City
  * Store
  * User
* Control UI visibility based on permissions

---

## 🛠️ Tech Stack

| Layer       | Technology            |
| ----------- | --------------------- |
| Backend     | ASP.NET MVC (.NET)    |
| Database    | SQL Server            |
| Data Access | ADO.NET               |
| Frontend    | HTML, CSS, JavaScript |
| UI Behavior | AJAX                  |

---

## 🏗️ Project Architecture

```
Controllers  → Handle requests
Services     → Business logic
DAL          → Database operations
Models       → Data structures
Views        → UI (Razor)
Attributes   → Custom authorization
```

---

## 📸 Screenshots

### 🖥️ Admin Dashboard

<p align="center">
  <img src="https://github.com/user-attachments/assets/3f1a5e26-a20c-4be2-8761-fcbf5e390d7c" width="800"/>
</p>

### 🌍 Country Management

<p align="center">
  <img src="https://github.com/user-attachments/assets/da7aa99b-91bd-491d-b614-9e5fd95d65fe" width="800"/>
</p>

### 🏙️ State Management

<p align="center">
  <img src="https://github.com/user-attachments/assets/83859ba0-776a-4e06-9f67-3e2b5030c061" width="800"/>
</p>

### 🏙️ City Management

<p align="center">
  <img src="https://github.com/user-attachments/assets/8b313266-4daa-4b92-ba70-7873dd05c26b" width="800"/>
</p>

### 🏪 Store Management + Maps

<p align="center">
  <img src="https://github.com/user-attachments/assets/455add3d-a5be-4041-8494-8252e92d71a6" width="800"/>
</p>

### 📍 Real Location (Google Maps)

<p align="center">
  <img src="https://github.com/user-attachments/assets/48ef8c38-9bce-41d8-a923-49faa0426ceb" width="800"/>
</p>

---

## ⚙️ Setup Guide

### 1️⃣ Clone the repository

```bash
git clone https://github.com/your-username/Country_Store.git
```

### 2️⃣ Open in Visual Studio

### 3️⃣ Configure Database

Update `appsettings.json`:

```json
"DefaultConnection": "your_connection_string"
```

### 4️⃣ Run the project

---

## 🔥 Key Highlight

👉 This project demonstrates **real-world enterprise concepts**:

* Role-based access control (RBAC)
* Layered architecture
* Clean UI + UX
* Integration with external services (Google Maps)

---

## ⭐ Support

If you found this project helpful:

👉 Give it a ⭐ on GitHub


## 👩‍💻 Author

**Aastha Bhojani**

---

