/* ============================================================
   PRUEBA TÉCNICA - GESTIÓN DE CLIENTES
   Base de datos
   ============================================================ */

USE master;
GO

/* ------------------------------------------------------------
   Crear base de datos
   ------------------------------------------------------------ */

IF DB_ID(N'PruebaTecnicaClientes') IS NULL
BEGIN
    CREATE DATABASE PruebaTecnicaClientes;
END
GO

USE PruebaTecnicaClientes;
GO


/* ============================================================
   TABLA: Usuarios
   ============================================================ */

IF OBJECT_ID(N'dbo.Usuarios', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Usuarios
    (
        IdUsuario INT IDENTITY(1,1) NOT NULL,
        
        NombreUsuario NVARCHAR(50) NOT NULL,
        
        PasswordHash NVARCHAR(255) NOT NULL,
        
        NombreCompleto NVARCHAR(100) NOT NULL,
        
        Activo BIT NOT NULL
            CONSTRAINT DF_Usuarios_Activo DEFAULT (1),

        IntentosFallidos INT NOT NULL
            CONSTRAINT DF_Usuarios_IntentosFallidos DEFAULT (0),

        BloqueadoHasta DATETIME2(0) NULL,

        FechaCreacion DATETIME2(0) NOT NULL
            CONSTRAINT DF_Usuarios_FechaCreacion
            DEFAULT (SYSUTCDATETIME()),

        CONSTRAINT PK_Usuarios
            PRIMARY KEY (IdUsuario),

        CONSTRAINT UQ_Usuarios_NombreUsuario
            UNIQUE (NombreUsuario)
    );
END
GO


/* ============================================================
   TABLA: Clientes
   ============================================================ */

IF OBJECT_ID(N'dbo.Clientes', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Clientes
    (
        IdCliente INT IDENTITY(1,1) NOT NULL,

        Nombres NVARCHAR(100) NOT NULL,

        Apellidos NVARCHAR(100) NOT NULL,

        Documento NVARCHAR(30) NOT NULL,

        Telefono NVARCHAR(30) NULL,

        Correo NVARCHAR(150) NULL,

        Direccion NVARCHAR(250) NULL,

        Activo BIT NOT NULL
            CONSTRAINT DF_Clientes_Activo DEFAULT (1),

        FechaCreacion DATETIME2(0) NOT NULL
            CONSTRAINT DF_Clientes_FechaCreacion
            DEFAULT (SYSUTCDATETIME()),

        FechaModificacion DATETIME2(0) NULL,

        RowVersion ROWVERSION NOT NULL,

        CONSTRAINT PK_Clientes
            PRIMARY KEY (IdCliente),

        CONSTRAINT UQ_Clientes_Documento
            UNIQUE (Documento)
    );
END
GO


/* ============================================================
   TABLA: Bitacora
   ============================================================ */

IF OBJECT_ID(N'dbo.Bitacora', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Bitacora
    (
        IdBitacora INT IDENTITY(1,1) NOT NULL,

        Accion NVARCHAR(20) NOT NULL,

        IdCliente INT NOT NULL,

        IdUsuario INT NOT NULL,

        NombreUsuario NVARCHAR(50) NOT NULL,

        FechaHora DATETIME2(0) NOT NULL
            CONSTRAINT DF_Bitacora_FechaHora
            DEFAULT (SYSUTCDATETIME()),

        Detalle NVARCHAR(1000) NULL,

        CONSTRAINT PK_Bitacora
            PRIMARY KEY (IdBitacora),

        CONSTRAINT CK_Bitacora_Accion
            CHECK (Accion IN (N'AGREGAR', N'EDITAR', N'ELIMINAR')),

        CONSTRAINT FK_Bitacora_Cliente
            FOREIGN KEY (IdCliente)
            REFERENCES dbo.Clientes(IdCliente),

        CONSTRAINT FK_Bitacora_Usuario
            FOREIGN KEY (IdUsuario)
            REFERENCES dbo.Usuarios(IdUsuario)
    );
END
GO


/* ============================================================
   ÍNDICES
   ============================================================ */

IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = N'IX_Clientes_Nombres'
      AND object_id = OBJECT_ID(N'dbo.Clientes')
)
BEGIN
    CREATE INDEX IX_Clientes_Nombres
        ON dbo.Clientes(Nombres, Apellidos);
END
GO


IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = N'IX_Bitacora_IdCliente'
      AND object_id = OBJECT_ID(N'dbo.Bitacora')
)
BEGIN
    CREATE INDEX IX_Bitacora_IdCliente
        ON dbo.Bitacora(IdCliente);
END
GO


IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = N'IX_Bitacora_FechaHora'
      AND object_id = OBJECT_ID(N'dbo.Bitacora')
)
BEGIN
    CREATE INDEX IX_Bitacora_FechaHora
        ON dbo.Bitacora(FechaHora);
END
GO


PRINT 'Base de datos creada correctamente.';
PRINT 'Tablas Usuarios, Clientes y Bitacora creadas correctamente.';
GO