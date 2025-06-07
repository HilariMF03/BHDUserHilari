# BHDUsersHilari - Prueba Técnica API REST de Usuarios

## Descripción
Implementación de una API RESTful en ASP.NET Core (.NET 6+) para el registro y gestión de usuarios, respetando los requerimientos de la Prueba.

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
-git clone https://github.com/HilariMF03/BHDUserHilari.git
-cd BHDUserHilari\Source
2. Configura la conexión en `appsettings.json`.
-Abra "Presentation\WebApplication1\appsettings.json" y pon tu cadena de conexión bajo "ConnectionStrings".
-"Server=DESKTOP-IR687PC\\SQL2022;Database=BHDUsers;User ID=*;Password=*;MultipleActiveResultSets=true;TrustServerCertificate=True"
3. Ejecuta migraciones:
-Abra su terminal y dirijase al directorio del proyecto: cd BHDUserHilari\Source, luego: cd BHDUserHilari\Source
-dotnet ef migrations add InitialCreate o Add-migration NombreDeTuMigracion
-dotnet ef database update o Update-database
4. Arranca la API:
  
## Endpoints
### Registrar usuario
- **URL:** POST `/api/users`
- **Body:**
  ```json
  {
    "name": "Odris09",
    "email": "odris09@gmail.com",
    "password": "Secreto123",
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
