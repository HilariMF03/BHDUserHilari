# BHUsersHilari - Prueba Técnica API REST de Usuarios

## Descripción
Implementación de una API RESTful en ASP.NET Core (.NET 6+) para el registro y gestión de usuarios, respetando los requerimientos de la vacante.

## Estructura del Proyecto
```
Source/
  Core/
    Application/
      Dtos
      Interfaces
      Services
      ServiceRegistration.cs
    Domain/
      Dependencies
      Entities
  Infrastructure/
    Persistence/
      Context
      Migrations
      Repositories
      ServiceRegistration.cs
  Presentation/
    WebApplication1/
      Controllers
      appsettings.json
      Program.cs
      WebApplication1.http
```

# BHDUsersHilari - API REST Usuarios

## Instalación y ejecución
1. Clona el repositorio:
2. Configura la conexión en `appsettings.json`.
3. Ejecuta migraciones:
4. Arranca la API:
  

## Endpoints
### Registrar usuario
- **URL:** POST `/api/users`
- **Body:**
  ```json
  {
    "name": "Odris09",
    "email": "odris09@gmail.com",
    "password": "Secreto123!",
    "phones": [
      { "number": "1234567", "cityCode": "1", "contryCode": "58" }
    ]
  }
  ```
- **Respuesta (201):**
  ```json
  {
    "id": "06d52c10-a1e1-45e7-ae59-77e6ba9728eb",
    "created": "2025-06-07T02:49:49Z",
    "modified": "2025-06-07T02:49:49Z",
    "last_login": "2025-06-07T02:49:49Z",
    "token": "<JWT>",
    "isactive": true
  }
