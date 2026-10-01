# TestVB — Gestión de Clientes

Aplicación web desarrollada como solución para una prueba técnica de gestión de clientes.

El sistema permite autenticar usuarios y administrar clientes mediante operaciones de alta, consulta, edición, eliminación lógica y reactivación. Las modificaciones realizadas quedan registradas en una bitácora asociada al usuario que ejecutó la operación.

## Tecnologías

- ASP.NET Web Forms
- VB.NET
- .NET Framework 4.8
- SQL Server
- ADO.NET
- Bootstrap 5

## Funcionalidades

- Inicio y cierre de sesión.
- Autenticación de usuarios contra SQL Server.
- Listado y búsqueda de clientes.
- Registro y edición de clientes.
- Eliminación lógica.
- Reactivación de clientes previamente eliminados.
- Validación de documentos duplicados.
- Registro de modificaciones en bitácora.
- Validaciones de datos del lado servidor.
- Manejo centralizado de errores.

## Arquitectura

El proyecto mantiene una separación de responsabilidades entre la interfaz, la lógica de negocio y el acceso a datos:

```text
Pages
   ↓
Services
   ↓
Repositories
   ↓
SQL Server