# Página de Administración de Artículos

## Descripción

Se ha creado una página completa de administración para gestionar artículos en el frontend de la aplicación e-commerce.

## Ubicación

**Ruta:** `/admin/articulos`

**Componente:** `AdminArticulosComponent`

## Funcionalidades Implementadas

### ✅ 1. Crear Nuevo Artículo
Formulario completo para dar de alta artículos con los siguientes campos:
- **Código** (requerido): Identificador único del artículo
- **Descripción** (requerido): Nombre o descripción del producto
- **Precio** (requerido): Precio del artículo (acepta decimales)
- **Stock Inicial** (requerido): Cantidad inicial en inventario
- **URL de Imagen** (opcional): Link a la imagen del producto

### ✅ 2. Asignar Artículo a Tienda
Sección para relacionar artículos existentes con tiendas:
- Seleccionar artículo del dropdown
- Seleccionar tienda del dropdown
- Especificar stock disponible en esa tienda
- Botón para confirmar la asignación

### ✅ 3. Listado de Artículos
Tabla con todos los artículos registrados mostrando:
- Código
- Descripción
- Precio
- Stock
- Estado (Activo/Inactivo)
- Botón para eliminar

### ✅ 4. Validaciones
- Campos requeridos marcados con *
- Validación de números positivos
- Mensajes de éxito y error
- Confirmación antes de eliminar

## Cómo Acceder

1. **Iniciar sesión** en la aplicación
2. En el navbar superior, hacer clic en **"Admin Artículos"**
3. O navegar directamente a: `http://localhost:4200/admin/articulos`

## Endpoints de API Utilizados

La página consume los siguientes endpoints del backend:

```
GET    /api/articulos              - Listar todos los artículos
POST   /api/articulos              - Crear nuevo artículo
DELETE /api/articulos/{id}         - Eliminar artículo
POST   /api/articulos/asignar-tienda - Asignar artículo a tienda
GET    /api/tiendas                - Listar tiendas (para dropdown)
```

## Estructura de Archivos Creados

```
EcommerceApp.Frontend/src/app/
├── components/
│   └── admin-articulos/
│       ├── admin-articulos.component.ts      # Lógica del componente
│       ├── admin-articulos.component.html    # Template HTML
│       └── admin-articulos.component.css     # Estilos
├── services/
│   └── articulo.service.ts                   # Actualizado con método asignarATienda
└── app.routes.ts                             # Actualizado con nueva ruta
```

## Ejemplo de Uso

### Crear un Artículo:

1. Llenar el formulario:
   - Código: `LAPTOP001`
   - Descripción: `Laptop HP 15.6"`
   - Precio: `599.99`
   - Stock: `10`
   - Imagen: `https://ejemplo.com/laptop.jpg`

2. Hacer clic en **"Crear Artículo"**

3. El artículo aparecerá en la tabla inferior

### Asignar a Tienda:

1. Seleccionar el artículo creado del dropdown
2. Seleccionar una tienda
3. Ingresar el stock disponible en esa tienda (ej: `5`)
4. Hacer clic en **"Asignar a Tienda"**

## Características de la Interfaz

- **Diseño Responsivo**: Se adapta a diferentes tamaños de pantalla
- **Mensajes de Feedback**: Alertas de éxito y error
- **Validación en Tiempo Real**: Los botones se deshabilitan si faltan datos
- **Tabla Interactiva**: Hover effects y acciones rápidas
- **Badges de Estado**: Indicadores visuales de activo/inactivo

## Protección de Ruta

La página está protegida con `authGuard`, lo que significa que:
- Solo usuarios autenticados pueden acceder
- Si no hay sesión activa, redirige automáticamente al login

## Mejoras Futuras Sugeridas

- [ ] Agregar edición de artículos existentes
- [ ] Implementar búsqueda y filtros
- [ ] Agregar paginación para muchos artículos
- [ ] Permitir carga de imágenes desde el dispositivo
- [ ] Agregar vista previa de imagen
- [ ] Implementar permisos de administrador
- [ ] Agregar exportación a Excel/PDF
- [ ] Historial de cambios en artículos

## Notas Técnicas

- Utiliza **Standalone Components** de Angular
- Implementa **Reactive Forms** con FormsModule
- Consume servicios HTTP con **HttpClient**
- Usa **RxJS Observables** para operaciones asíncronas
- Aplica el patrón de **Inyección de Dependencias**

## Solución de Problemas

### Error: "No se pueden cargar los artículos"
- Verificar que el backend esté corriendo en `https://localhost:7000`
- Revisar que la base de datos esté actualizada
- Verificar el token JWT en localStorage

### Error: "No se pueden cargar las tiendas"
- Asegurarse de que existan tiendas en la base de datos
- Crear al menos una tienda usando el endpoint correspondiente

### Los cambios no se reflejan
- Hacer hard refresh (Ctrl + Shift + R)
- Verificar la consola del navegador para errores
- Revisar que el servicio ArticuloService esté correctamente inyectado

## Contacto y Soporte

Para reportar bugs o sugerir mejoras, contactar al equipo de desarrollo.
