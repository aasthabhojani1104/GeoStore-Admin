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

### 🖥️ Admin Pannel
### 🌍 Country Management

<p align="center">
 <img width="959" height="452" alt="image" src="https://github.com/user-attachments/assets/4ebc8488-434d-4485-b9c9-2ed09702e37f" />

</p>



### 🏙️ State Management

<p align="center">
 <img width="959" height="406" alt="image" src="https://github.com/user-attachments/assets/69f2f817-d5f6-4ca7-8a7d-2bfaa35711e2" />

</p>

### 🏙️ City Management

<p align="center">
 <img width="959" height="423" alt="image" src="https://github.com/user-attachments/assets/53e76f4c-d7df-4060-847e-f334d10237d1" />

</p>

### 🏪 Store Management + Maps

<p align="center">
 <img width="959" height="452" alt="image" src="https://github.com/user-attachments/assets/218354e4-f17b-498b-8cf6-0bea09705f3f" />

</p>

### 📍 Real Location (Google Maps)

<p align="center">
<img width="959" height="451" alt="image" src="https://github.com/user-attachments/assets/eb6c838c-7630-42df-88c6-43aa97da533f" />


</p>
### 📋 All Lists Overview

<p align="center">
  <img src="https://github.com/user-attachments/assets/52c90677-2bf2-4812-878a-1b7833090b4b" width="959" height="451"/>
</p>

<p align="center">
  <img src="https://github.com/user-attachments/assets/0c2ab67e-7e76-451c-84f4-857a6dbfc313" width="900"/>
</p>

---


<p align="center">
  <img src="https://github.com/user-attachments/assets/b7cffdf3-3f53-47c6-bfbe-045eefdc6d7a" width="900"/>
</p>

<p align="center">
  <img src="https://github.com/user-attachments/assets/e2ee78d3-c642-45a3-8299-ffee457ff123" width="900"/>
</p>

## ⚙️ Setup Guide

### 1️⃣ Clone the repository

```bash
git clone https://github.com/aasthabhojani1104/Country_Store.git
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

