# 🚀 BHDUsersHilari – Prueba Técnica API REST de Usuarios

Implementación de una API RESTful utilizando **ASP.NET Core (.NET 6+)** para el registro y gestión de usuarios. Este proyecto fue desarrollado como parte de una prueba técnica y cumple con todos los requerimientos solicitados.

---

## 📁 Estructura del Proyecto

```
Source/
├── Core/
│   ├── Application/
│   │   ├── Dtos
│   │   ├── Interfaces
│   │   ├── Services
│   │   └── ServiceRegistration.cs
│   └── Domain/
│       ├── Dependencies
│       └── Entities
├── Infrastructure/
│   └── Persistence/
│       ├── Context
│       ├── Migrations
│       ├── Repositories
│       └── ServiceRegistration.cs
└── Presentation/
    └── WebApplication1/
        ├── Controllers
        ├── appsettings.json
        ├── Program.cs
        └── WebApplication1.http
```

---

## ⚙️ Instalación y Ejecución

### 1. Clona el repositorio

```bash
git clone https://github.com/HilariMF03/BHDUserHilari.git
cd BHDUserHilari/Source
```

### 2. Configura la conexión a la base de datos

Edita el archivo `Presentation/WebApplication1/appsettings.json` y reemplaza la cadena de conexión en `ConnectionStrings`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=DESKTOP-IR687PC\\SQL2022;Database=BHDUsers;User ID=USUARIO;Password=CONTRASEÑA;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

### 3. Ejecuta las migraciones

```bash
cd Presentation/WebApplication1
dotnet ef migrations add InitialCreate o add-migration {NombreDeTuMigracion}
dotnet ef database update o update-database
```

### 4. Inicia la API

```bash
dotnet run
```

La API estará disponible en: `https://localhost:{puerto}`

### 5. Ejecutar pruebas

```bash
Desde-su-terminal: dotnet test
```

---


## 📬 Endpoint Principal

### ➕ Registrar Usuario

- **Método:** `POST`  
- **URL:** `/api/users`

#### 📦 Request Body

```json
{
  "name": "Odris09",
  "email": "odris09@gmail.com",
  "password": "Secreto123",
  "phones": [
    {
      "number": "1234567",
      "cityCode": "1",
      "contryCode": "58"
    }
  ]
}
```

#### ✅ Respuesta Exitosa (201)

```json
{
  "id": "06d52c10-a1e1-45e7-ae59-77e6ba9728eb",
  "created": "2025-06-07T02:49:49Z",
  "modified": "2025-06-07T02:49:49Z",
  "last_login": "2025-06-07T02:49:49Z",
  "token": "<JWT>",
  "isactive": true
}
```

---

## 🧰 Requisitos Técnicos

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- SQL Server
- Entity Framework Core Tools (`dotnet tool install --global dotnet-ef`)
- IDE recomendado: Visual Studio 2022 o Visual Studio Code

---

## 📌 Notas Finales

- Se utilizó una arquitectura limpia por capas para mantener la separación de responsabilidades.
- La autenticación se maneja mediante token JWT (estructura disponible en la respuesta del registro).
- Se pueden incluir más endpoints como login, actualización o eliminación de usuario en futuras versiones.

---

## 🙋‍♀️ Contacto

**Hilari Medina Feliz**  
📧 hilarimedina0922@gmail.com  
🔗 [GitHub](https://github.com/HilariMF03)
