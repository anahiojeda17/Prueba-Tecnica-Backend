# Prueba Técnica Backend - API REST 

## Versión de .NET
.NET 8 

## Cómo correr el proyecto
1. Clonar el repositorio
2. Entrar a la carpeta del proyecto:
cd PruebaTecnica

## Cómo crear/aplicar migraciones SQLite
### Primera vez (crear la base de datos)
1. Instalar la herramienta de Entity Framework:
dotnet tool install --global dotnet-ef

2. Crear la migración:
dotnet ef migrations add InitialCreate

3. Aplicar la migración y crear la base de datos:
dotnet ef database update
Esto crea automáticamente el archivo app.db con todas las tablas.

4. Correr el proyecto:
dotnet run

## API Key de prueba y ejemplos de headers
La API Key para pruebas es: pruebatecnica_2026
### Desde Swagger
1. Entrar a http://localhost:5224/swagger
2. Hacer click en el botón Authorize 
3. Ingresar: pruebatecnica_2026
4. Click en Authorize y cerrar

## Lo que está implementado
- CRUD completo de Users
- CRUD completo de Addresses relacionados a Users
- CRUD de Currencies
- Conversión de divisas
- Seguridad por API Key
- Entity Framework Core con SQLite
- FluentValidation
- Patrón CQRS
- Swagger
