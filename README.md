# E-Commerce Application

Aplicación completa de e-commerce desarrollada con .NET Core (backend) y Angular (frontend).

## Arquitectura

### Backend (.NET Core 9.0)
- **EcommerceApp.Entities**: Modelos de entidades
- **EcommerceApp.Data**: Capa de acceso a datos con Entity Framework Core
- **EcommerceApp.Business**: Lógica de negocio y servicios
- **EcommerceApp.API**: API REST con controladores

### Frontend (Angular)
- **EcommerceApp.Frontend**: Aplicación Angular con componentes standalone

## Base de Datos

### Tablas Principales:
1. **Clientes**: Información de clientes con autenticación
2. **Tiendas**: Sucursales de la tienda
3. **Artículos**: Productos disponibles

### Relaciones:
- **ArticuloTienda**: Relación muchos a muchos entre Artículos y Tiendas
- **ClienteArticulo**: Historial de compras de clientes
- **Carrito/CarritoItem**: Sistema de carrito de compras

## Funcionalidades Implementadas

### Backend:
✅ CRUD completo de Tiendas
✅ CRUD completo de Artículos con relación a Tiendas
✅ CRUD completo de Clientes
✅ Sistema de autenticación con JWT
✅ Carrito de compras funcional
✅ Gestión de stock
✅ Historial de compras

### Frontend:
✅ Login y Registro de clientes
✅ Listado de productos
✅ Carrito de compras interactivo
✅ Navegación con barra superior
✅ Interceptor HTTP para JWT
✅ Servicios para todas las entidades

## Configuración y Ejecución

### Requisitos Previos:
- .NET SDK 9.0 o superior
- Node.js 18+ y npm
- SQL Server (LocalDB o instancia completa)
- Angular CLI

### Pasos para ejecutar el Backend:

1. **Configurar la cadena de conexión**:
   Editar `EcommerceApp.API/appsettings.json` y ajustar la cadena de conexión si es necesario:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=EcommerceDB;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```

2. **Crear la base de datos**:
   ```bash
   cd EcommerceApp
   dotnet ef database update --project EcommerceApp.Data --startup-project EcommerceApp.API
   ```

3. **Ejecutar la API**:
   ```bash
   cd EcommerceApp.API
   dotnet run
   ```
   La API estará disponible en: `https://localhost:7000`
   Swagger UI: `https://localhost:7000/swagger`

### Pasos para ejecutar el Frontend:

1. **Instalar dependencias**:
   ```bash
   cd EcommerceApp.Frontend
   npm install
   ```

2. **Actualizar app.html** (importante):
   Reemplazar el contenido de `src/app/app.html` con:
   ```html
   <app-navbar></app-navbar>
   <router-outlet></router-outlet>
   ```

3. **Ejecutar la aplicación**:
   ```bash
   npm start
   ```
   O:
   ```bash
   ng serve
   ```
   La aplicación estará disponible en: `http://localhost:4200`

## Endpoints de la API

### Autenticación:
- `POST /api/auth/register` - Registrar nuevo cliente
- `POST /api/auth/login` - Iniciar sesión

### Tiendas:
- `GET /api/tiendas` - Listar todas las tiendas
- `GET /api/tiendas/{id}` - Obtener tienda por ID
- `POST /api/tiendas` - Crear nueva tienda (requiere autenticación)
- `PUT /api/tiendas/{id}` - Actualizar tienda (requiere autenticación)
- `DELETE /api/tiendas/{id}` - Eliminar tienda (requiere autenticación)

### Artículos:
- `GET /api/articulos` - Listar todos los artículos
- `GET /api/articulos/{id}` - Obtener artículo por ID
- `GET /api/articulos/tienda/{tiendaId}` - Artículos por tienda
- `POST /api/articulos` - Crear nuevo artículo (requiere autenticación)
- `PUT /api/articulos/{id}` - Actualizar artículo (requiere autenticación)
- `DELETE /api/articulos/{id}` - Eliminar artículo (requiere autenticación)
- `POST /api/articulos/asignar-tienda` - Asignar artículo a tienda (requiere autenticación)

### Clientes:
- `GET /api/clientes` - Listar clientes (requiere autenticación)
- `GET /api/clientes/{id}` - Obtener cliente por ID (requiere autenticación)
- `PUT /api/clientes/{id}` - Actualizar cliente (requiere autenticación)
- `DELETE /api/clientes/{id}` - Eliminar cliente (requiere autenticación)

### Carrito:
- `GET /api/carrito` - Obtener carrito activo (requiere autenticación)
- `POST /api/carrito/agregar-item` - Agregar item al carrito (requiere autenticación)
- `PUT /api/carrito/actualizar-item` - Actualizar cantidad de item (requiere autenticación)
- `DELETE /api/carrito/remover-item/{itemId}` - Remover item del carrito (requiere autenticación)
- `POST /api/carrito/completar-compra` - Completar compra (requiere autenticación)

## Estructura del Proyecto

```
EcommerceApp/
├── EcommerceApp.Entities/          # Modelos de dominio
│   ├── Cliente.cs
│   ├── Tienda.cs
│   ├── Articulo.cs
│   ├── ArticuloTienda.cs
│   ├── ClienteArticulo.cs
│   ├── Carrito.cs
│   └── CarritoItem.cs
├── EcommerceApp.Data/              # Capa de datos
│   ├── ApplicationDbContext.cs
│   ├── Interfaces/
│   └── Repositories/
├── EcommerceApp.Business/          # Lógica de negocio
│   ├── DTOs/
│   ├── Interfaces/
│   └── Services/
├── EcommerceApp.API/               # API REST
│   ├── Controllers/
│   ├── Program.cs
│   └── appsettings.json
└── EcommerceApp.Frontend/          # Frontend Angular
    └── src/
        └── app/
            ├── components/
            ├── services/
            ├── models/
            └── interceptors/
```

## Tecnologías Utilizadas

### Backend:
- .NET 9.0
- Entity Framework Core 9.0
- SQL Server
- JWT Authentication
- BCrypt.Net para hash de contraseñas
- Swagger/OpenAPI

### Frontend:
- Angular 19 (Standalone Components)
- TypeScript
- RxJS
- Angular Router
- HTTP Client

## Notas Importantes

1. **Seguridad**: Cambiar el secreto JWT en producción (`appsettings.json`)
2. **CORS**: Configurado para aceptar peticiones desde `http://localhost:4200`
3. **Base de Datos**: La migración inicial está creada, solo ejecutar `dotnet ef database update`
4. **Autenticación**: El token JWT se almacena en localStorage del navegador
5. **Stock**: El sistema valida stock disponible antes de permitir compras

## Próximos Pasos Sugeridos

- Agregar paginación en los listados
- Implementar filtros y búsqueda
- Agregar panel de administración
- Implementar carga de imágenes
- Agregar validaciones más robustas
- Implementar tests unitarios
- Agregar logging
- Configurar para producción

## Autor

Desarrollado como proyecto de demostración de arquitectura en capas con .NET Core y Angular.
