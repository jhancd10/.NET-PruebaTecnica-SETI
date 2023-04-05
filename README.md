# .NET-PruebaTecnica-SETI

REST Api en .NET Core 6 usando:

* Principios SOLID.
* Inyección de Dependencias.
* Entity Framework Core.
* ADO Connection
* Patron de Diseño: **Strategy**.
* Programación Asincrona y Concurrente

## Requerimientos:
1. Consultar los proyectos de menor tiempo de recuperación de la inversión.
2. Consultar los proyectos de mayor beneficio generado por la inversión.

-------------------------------------------------------------------------------------------------------------------------------------------------------

## Despliegue: Localhost

1. Descargar e Instalar SQL Server Express: https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15
2. Conectarse al Servidor de BD con las credenciales de conexión enviadas para el desarrollo de la prueba.
3. Descargar e Instalar ASP.NET Core Runtime: https://dotnet.microsoft.com/es-es/download/dotnet/thank-you/runtime-aspnetcore-6.0.15-windows-hosting-bundle-installer
4. Clonar este repositorio.
5. En la carpeta donde clone el repositorio dirigirse a la ruta: **SETI-Backend --> SETI.WebApi --> bin --> Release --> net6.0 --> publish**
6. Click derecho y seleccionar **'Abrir en Consola'**
7. En la consola escribir el siguiente comando: dotnet **SETI.WebApi.dll**
8. Esperar que inicie el servidor local.
9. Para consultar los proyectos de menor tiempo de recuperación de la inversión, realizar una petición **GET** al sgte endpoint: **https://localhost:5001/api/reportes/tiemporecuperacioninversion**
10. Para consultar los proyectos de mayor beneficio generado por la inversión, realizar una petición **GET** al sgte endpoint: **https://localhost:5001/api/reportes/beneficiogeneradoinversion**

-------------------------------------------------------------------------------------------------------------------------------------------------------

## Solución y revisión de Código:

1. Clonar este repositorio.
2. En la carpeta donde clone el repositorio dirigirse a la ruta: **SETI-Backend --> SETI.WebApi**
3. Abrir el proyecto **SETI.WebApi.sln**
4. Limpiar Solución.
5. Compilar, Recompilar.
