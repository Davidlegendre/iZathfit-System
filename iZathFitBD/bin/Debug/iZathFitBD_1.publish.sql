/*
Script de implementación para izathfitbd

Una herramienta generó este código.
Los cambios realizados en este archivo podrían generar un comportamiento incorrecto y se perderán si
se vuelve a generar el código.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "izathfitbd"
:setvar DefaultFilePrefix "izathfitbd"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detectar el modo SQLCMD y deshabilitar la ejecución del script si no se admite el modo SQLCMD.
Para volver a habilitar el script después de habilitar el modo SQLCMD, ejecute lo siguiente:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'El modo SQLCMD debe estar habilitado para ejecutar correctamente este script.';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creando la base de datos $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'No se puede modificar la configuración de la base de datos. Debe ser un administrador del sistema para poder aplicar esta configuración.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'No se puede modificar la configuración de la base de datos. Debe ser un administrador del sistema para poder aplicar esta configuración.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_PLANS_PER_QUERY = 200, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET TEMPORAL_HISTORY_RETENTION ON 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Creando Tabla [dbo].[Configuracion]...';


GO
CREATE TABLE [dbo].[Configuracion] (
    [IdConfiguracion] INT           IDENTITY (1, 1) NOT NULL,
    [NombreEmpresa]   VARCHAR (120) NOT NULL,
    [Direccion]       VARCHAR (100) NULL,
    [Correo]          VARCHAR (255) NULL,
    [Descripcion]     VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([IdConfiguracion] ASC)
);


GO
PRINT N'Creando Tabla [dbo].[Contrato]...';


GO
CREATE TABLE [dbo].[Contrato] (
    [IdContrato]     UNIQUEIDENTIFIER NOT NULL,
    [IdPlan]         INT              NOT NULL,
    [IdPersona]      UNIQUEIDENTIFIER NOT NULL,
    [IdUsuario]      UNIQUEIDENTIFIER NOT NULL,
    [ValorTotal]     DECIMAL (18, 2)  NOT NULL,
    [Descuento]      INT              NULL,
    [ValorOriginal]  DECIMAL (18, 2)  NOT NULL,
    [FechaInicio]    DATETIME         NOT NULL,
    [FechaFinal]     DATETIME         NOT NULL,
    [Fecha_contrato] DATETIME         NOT NULL,
    [NumeroContrato] CHAR (6)         NOT NULL,
    [IdTipoPago]     INT              NOT NULL,
    PRIMARY KEY CLUSTERED ([IdContrato] ASC),
    UNIQUE NONCLUSTERED ([NumeroContrato] ASC)
);


GO
PRINT N'Creando Tabla [dbo].[Genero]...';


GO
CREATE TABLE [dbo].[Genero] (
    [IdGenero]    INT          IDENTITY (1, 1) NOT NULL,
    [descripcion] VARCHAR (10) NOT NULL,
    [code]        CHAR (1)     NOT NULL,
    PRIMARY KEY CLUSTERED ([IdGenero] ASC)
);


GO
PRINT N'Creando Tabla [dbo].[NumEmergencias]...';


GO
CREATE TABLE [dbo].[NumEmergencias] (
    [IdPersona] UNIQUEIDENTIFIER NOT NULL,
    [Nombres]   VARCHAR (50)     NOT NULL,
    [Apellido]  VARCHAR (50)     NOT NULL,
    [Celular]   VARCHAR (10)     NOT NULL
);


GO
PRINT N'Creando Tabla [dbo].[Ocupacion]...';


GO
CREATE TABLE [dbo].[Ocupacion] (
    [IdOcupacion] INT           IDENTITY (1, 1) NOT NULL,
    [descripcion] VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdOcupacion] ASC)
);


GO
PRINT N'Creando Tabla [dbo].[OcupacionXPersona]...';


GO
CREATE TABLE [dbo].[OcupacionXPersona] (
    [IdPersona]   UNIQUEIDENTIFIER NOT NULL,
    [IdOcupacion] INT              NOT NULL
);


GO
PRINT N'Creando Tabla [dbo].[PadecimientoEnfermedades]...';


GO
CREATE TABLE [dbo].[PadecimientoEnfermedades] (
    [IdPersona]    UNIQUEIDENTIFIER NOT NULL,
    [Padecimiento] VARCHAR (100)    NOT NULL
);


GO
PRINT N'Creando Tabla [dbo].[Permisos]...';


GO
CREATE TABLE [dbo].[Permisos] (
    [IdUsuario]  UNIQUEIDENTIFIER NOT NULL,
    [IdVentana]  INT              NOT NULL,
    [Crear]      BIT              NOT NULL,
    [Leer]       BIT              NOT NULL,
    [Actualizar] BIT              NOT NULL,
    [Eliminar]   BIT              NOT NULL
);


GO
PRINT N'Creando Tabla [dbo].[Persona]...';


GO
CREATE TABLE [dbo].[Persona] (
    [IdPersona]            UNIQUEIDENTIFIER NOT NULL,
    [Nombres]              VARCHAR (50)     NOT NULL,
    [Apellidos]            VARCHAR (50)     NOT NULL,
    [Fech_Nacimiento]      DATETIME         NOT NULL,
    [Edad]                 AS               (Year(Getdate()) - Year(Fech_Nacimiento)),
    [Direccion]            VARCHAR (100)    NOT NULL,
    [Telefono]             CHAR (9)         NOT NULL,
    [Email]                VARCHAR (255)    NULL,
    [idGenero]             INT              NOT NULL,
    [Identificacion]       VARCHAR (20)     NOT NULL,
    [idtipoIdentificacion] INT              NOT NULL,
    [idRol]                INT              NOT NULL,
    PRIMARY KEY CLUSTERED ([IdPersona] ASC),
    UNIQUE NONCLUSTERED ([Identificacion] ASC)
);


GO
PRINT N'Creando Tabla [dbo].[PlanDuracion]...';


GO
CREATE TABLE [dbo].[PlanDuracion] (
    [IdPlanDuracion] INT              IDENTITY (1, 1) NOT NULL,
    [descripcion]    VARCHAR (10)     NOT NULL,
    [IdUsuario]      UNIQUEIDENTIFIER NOT NULL,
    [MesesTiempo]    INT              NOT NULL,
    PRIMARY KEY CLUSTERED ([IdPlanDuracion] ASC)
);


GO
PRINT N'Creando Tabla [dbo].[Planes]...';


GO
CREATE TABLE [dbo].[Planes] (
    [IdPlanes]       INT              IDENTITY (1, 1) NOT NULL,
    [IdServicio]     INT              NOT NULL,
    [IdPlanDuracion] INT              NOT NULL,
    [IdUsuario]      UNIQUEIDENTIFIER NOT NULL,
    [descripcion]    VARCHAR (100)    NULL,
    [Precio]         DECIMAL (18, 2)  NOT NULL,
    PRIMARY KEY CLUSTERED ([IdPlanes] ASC)
);


GO
PRINT N'Creando Tabla [dbo].[Promociones]...';


GO
CREATE TABLE [dbo].[Promociones] (
    [IdPromocion]      INT              IDENTITY (1, 1) NOT NULL,
    [IdPlan]           INT              NOT NULL,
    [IdUsuario]        UNIQUEIDENTIFIER NOT NULL,
    [DescuentoPercent] INT              NOT NULL,
    [IsActivo]         BIT              NOT NULL,
    [DuracionTiempo]   DATETIME         NOT NULL,
    [descripcion]      VARCHAR (100)    NULL,
    PRIMARY KEY CLUSTERED ([IdPromocion] ASC)
);


GO
PRINT N'Creando Tabla [dbo].[RedesSociales]...';


GO
CREATE TABLE [dbo].[RedesSociales] (
    [IdRedSocial]     INT           IDENTITY (1, 1) NOT NULL,
    [IdConfiguracion] INT           NOT NULL,
    [idTipoRedSocial] INT           NOT NULL,
    [redsocial]       VARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdRedSocial] ASC)
);


GO
PRINT N'Creando Tabla [dbo].[Rol]...';


GO
CREATE TABLE [dbo].[Rol] (
    [IdRol]       INT          IDENTITY (1, 1) NOT NULL,
    [descripcion] VARCHAR (50) NOT NULL,
    [code]        CHAR (4)     NOT NULL,
    PRIMARY KEY CLUSTERED ([IdRol] ASC),
    UNIQUE NONCLUSTERED ([code] ASC)
);


GO
PRINT N'Creando Tabla [dbo].[Saldos]...';


GO
CREATE TABLE [dbo].[Saldos] (
    [IdPersona]  UNIQUEIDENTIFIER NOT NULL,
    [IsVencido]  BIT              NOT NULL,
    [IdPlan]     INT              NOT NULL,
    [ValorTotal] DECIMAL (18, 2)  NOT NULL,
    [PagoActual] DECIMAL (18, 2)  NOT NULL
);


GO
PRINT N'Creando Tabla [dbo].[Servicio]...';


GO
CREATE TABLE [dbo].[Servicio] (
    [IdServicio]     INT              IDENTITY (1, 1) NOT NULL,
    [NombreServicio] VARCHAR (100)    NOT NULL,
    [HorarioInicio]  DATETIME         NOT NULL,
    [HorarioFin]     DATETIME         NOT NULL,
    [IsGrupal]       BIT              NOT NULL,
    [IdUsuario]      UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([IdServicio] ASC)
);


GO
PRINT N'Creando Tabla [dbo].[TipoIdentity]...';


GO
CREATE TABLE [dbo].[TipoIdentity] (
    [IdTipoIdentity] INT          IDENTITY (1, 1) NOT NULL,
    [descripcion]    VARCHAR (50) NOT NULL,
    [abreviado]      VARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdTipoIdentity] ASC)
);


GO
PRINT N'Creando Tabla [dbo].[TipoPago]...';


GO
CREATE TABLE [dbo].[TipoPago] (
    [IdtipoPago]  INT           IDENTITY (1, 1) NOT NULL,
    [descripcion] VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdtipoPago] ASC)
);


GO
PRINT N'Creando Tabla [dbo].[TipoRedSocial]...';


GO
CREATE TABLE [dbo].[TipoRedSocial] (
    [IdTipoRedSocial] INT           IDENTITY (1, 1) NOT NULL,
    [descripcion]     VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdTipoRedSocial] ASC)
);


GO
PRINT N'Creando Tabla [dbo].[Usuario]...';


GO
CREATE TABLE [dbo].[Usuario] (
    [idUsuario]  UNIQUEIDENTIFIER NOT NULL,
    [IdPersona]  UNIQUEIDENTIFIER NOT NULL,
    [usuario]    VARCHAR (10)     NOT NULL,
    [contrasena] VARCHAR (16)     NOT NULL,
    [IsActivo]   BIT              NOT NULL,
    PRIMARY KEY CLUSTERED ([idUsuario] ASC),
    UNIQUE NONCLUSTERED ([IdPersona] ASC),
    UNIQUE NONCLUSTERED ([usuario] ASC)
);


GO
PRINT N'Creando Tabla [dbo].[Ventana]...';


GO
CREATE TABLE [dbo].[Ventana] (
    [idVentana] INT           IDENTITY (1, 1) NOT NULL,
    [Titulo]    VARCHAR (100) NOT NULL,
    [Codigo]    CHAR (8)      NOT NULL,
    PRIMARY KEY CLUSTERED ([idVentana] ASC)
);


GO
PRINT N'Creando Restricción DEFAULT restricción sin nombre en [dbo].[Contrato]...';


GO
ALTER TABLE [dbo].[Contrato]
    ADD DEFAULT (0) FOR [Descuento];


GO
PRINT N'Creando Restricción DEFAULT restricción sin nombre en [dbo].[Contrato]...';


GO
ALTER TABLE [dbo].[Contrato]
    ADD DEFAULT (getdate() + 1) FOR [FechaInicio];


GO
PRINT N'Creando Restricción DEFAULT restricción sin nombre en [dbo].[Contrato]...';


GO
ALTER TABLE [dbo].[Contrato]
    ADD DEFAULT (getdate()) FOR [Fecha_contrato];


GO
PRINT N'Creando Restricción DEFAULT restricción sin nombre en [dbo].[Promociones]...';


GO
ALTER TABLE [dbo].[Promociones]
    ADD DEFAULT (0) FOR [IsActivo];


GO
PRINT N'Creando Restricción DEFAULT restricción sin nombre en [dbo].[Saldos]...';


GO
ALTER TABLE [dbo].[Saldos]
    ADD DEFAULT (0) FOR [IsVencido];


GO
PRINT N'Creando Restricción DEFAULT restricción sin nombre en [dbo].[Servicio]...';


GO
ALTER TABLE [dbo].[Servicio]
    ADD DEFAULT (0) FOR [IsGrupal];


GO
PRINT N'Creando Restricción DEFAULT restricción sin nombre en [dbo].[Usuario]...';


GO
ALTER TABLE [dbo].[Usuario]
    ADD DEFAULT (1) FOR [IsActivo];


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[Contrato]...';


GO
ALTER TABLE [dbo].[Contrato]
    ADD FOREIGN KEY ([IdPlan]) REFERENCES [dbo].[Planes] ([IdPlanes]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[Contrato]...';


GO
ALTER TABLE [dbo].[Contrato]
    ADD FOREIGN KEY ([IdPersona]) REFERENCES [dbo].[Persona] ([IdPersona]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[Contrato]...';


GO
ALTER TABLE [dbo].[Contrato]
    ADD FOREIGN KEY ([IdTipoPago]) REFERENCES [dbo].[TipoPago] ([IdtipoPago]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[Contrato]...';


GO
ALTER TABLE [dbo].[Contrato]
    ADD FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuario] ([idUsuario]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[NumEmergencias]...';


GO
ALTER TABLE [dbo].[NumEmergencias]
    ADD FOREIGN KEY ([IdPersona]) REFERENCES [dbo].[Persona] ([IdPersona]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[OcupacionXPersona]...';


GO
ALTER TABLE [dbo].[OcupacionXPersona]
    ADD FOREIGN KEY ([IdPersona]) REFERENCES [dbo].[Persona] ([IdPersona]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[OcupacionXPersona]...';


GO
ALTER TABLE [dbo].[OcupacionXPersona]
    ADD FOREIGN KEY ([IdOcupacion]) REFERENCES [dbo].[Ocupacion] ([IdOcupacion]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[PadecimientoEnfermedades]...';


GO
ALTER TABLE [dbo].[PadecimientoEnfermedades]
    ADD FOREIGN KEY ([IdPersona]) REFERENCES [dbo].[Persona] ([IdPersona]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[Permisos]...';


GO
ALTER TABLE [dbo].[Permisos]
    ADD FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuario] ([idUsuario]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[Permisos]...';


GO
ALTER TABLE [dbo].[Permisos]
    ADD FOREIGN KEY ([IdVentana]) REFERENCES [dbo].[Ventana] ([idVentana]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[Persona]...';


GO
ALTER TABLE [dbo].[Persona]
    ADD FOREIGN KEY ([idGenero]) REFERENCES [dbo].[Genero] ([IdGenero]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[Persona]...';


GO
ALTER TABLE [dbo].[Persona]
    ADD FOREIGN KEY ([idtipoIdentificacion]) REFERENCES [dbo].[TipoIdentity] ([IdTipoIdentity]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[Persona]...';


GO
ALTER TABLE [dbo].[Persona]
    ADD FOREIGN KEY ([idRol]) REFERENCES [dbo].[Rol] ([IdRol]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[PlanDuracion]...';


GO
ALTER TABLE [dbo].[PlanDuracion]
    ADD FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuario] ([idUsuario]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[Planes]...';


GO
ALTER TABLE [dbo].[Planes]
    ADD FOREIGN KEY ([IdServicio]) REFERENCES [dbo].[Servicio] ([IdServicio]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[Planes]...';


GO
ALTER TABLE [dbo].[Planes]
    ADD FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuario] ([idUsuario]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[Planes]...';


GO
ALTER TABLE [dbo].[Planes]
    ADD FOREIGN KEY ([IdPlanDuracion]) REFERENCES [dbo].[PlanDuracion] ([IdPlanDuracion]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[Promociones]...';


GO
ALTER TABLE [dbo].[Promociones]
    ADD FOREIGN KEY ([IdPlan]) REFERENCES [dbo].[Planes] ([IdPlanes]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[Promociones]...';


GO
ALTER TABLE [dbo].[Promociones]
    ADD FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuario] ([idUsuario]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[RedesSociales]...';


GO
ALTER TABLE [dbo].[RedesSociales]
    ADD FOREIGN KEY ([IdConfiguracion]) REFERENCES [dbo].[Configuracion] ([IdConfiguracion]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[RedesSociales]...';


GO
ALTER TABLE [dbo].[RedesSociales]
    ADD FOREIGN KEY ([idTipoRedSocial]) REFERENCES [dbo].[TipoRedSocial] ([IdTipoRedSocial]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[Servicio]...';


GO
ALTER TABLE [dbo].[Servicio]
    ADD FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuario] ([idUsuario]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[Usuario]...';


GO
ALTER TABLE [dbo].[Usuario]
    ADD FOREIGN KEY ([IdPersona]) REFERENCES [dbo].[Persona] ([IdPersona]);


GO
PRINT N'Creando Restricción CHECK restricción sin nombre en [dbo].[PlanDuracion]...';


GO
ALTER TABLE [dbo].[PlanDuracion]
    ADD CHECK (MesesTiempo >= 1 and MesesTiempo <= 12);


GO
PRINT N'Creando Restricción CHECK restricción sin nombre en [dbo].[Promociones]...';


GO
ALTER TABLE [dbo].[Promociones]
    ADD CHECK (DescuentoPercent >= 1 and DescuentoPercent <= 100);


GO
PRINT N'Creando Procedimiento [dbo].[ActualizarVentana]...';


GO
create procedure ActualizarVentana
	@Titulo varchar(100),
	@Codigo char(8), @idVentana int
	as
	BEGIN
		update Ventana set Titulo = @Titulo, Codigo = @Codigo 
		where idVentana = @idVentana
	END
GO
PRINT N'Creando Procedimiento [dbo].[AgregarPersona]...';


GO
create procedure AgregarPersona
		@Nombres varchar(50), @Apellidos varchar(50),
		@Fech_nac datetime, @Direccion varchar(100),
		@idrol int, @telefono char(9),
		@Email varchar(255), @idgenero int, 
		@Identificacion varchar(20), @idTipoIdent int
	as
	BEGIN
		declare @uid uniqueidentifier
		set @uid = newid()
		insert into Persona(IdPersona, 
		Nombres, Apellidos, Fech_Nacimiento, Direccion, 
		idRol, Telefono, Email,idGenero,
		Identificacion,idtipoIdentificacion)
		values(@uid,@Nombres,@Apellidos,@Fech_nac,@Direccion,
		@idrol, @telefono,@Email,@idgenero,@Identificacion,@idTipoIdent)

		exec izathfitbd.dbo.SelectOnePersona @uid
	END
GO
PRINT N'Creando Procedimiento [dbo].[ChangePasswordUser]...';


GO
create procedure ChangePasswordUser
	@password varchar(16), @idpersona uniqueidentifier
	AS
	BEGIN
		declare @isequals varchar(16)
		set @isequals = (select u.contrasena from Usuario u where u.IdPersona = @idpersona)
		if(@isequals = @password)
			THROW 51000, 'La contraseña es la misma', 1;
		else
		BEGIN
			update Usuario set contrasena = @password where IdPersona = @idpersona
		END
	END
GO
PRINT N'Creando Procedimiento [dbo].[DeletePersona]...';


GO
create procedure DeletePersona
	@idPersona uniqueidentifier
	as
	BEGIN
		declare @exist uniqueidentifier
		set @exist = (select u.IdPersona from Usuario u where u.IdPersona = @idPersona)
		if(@exist is not null)
			THROW 51000, 'Esta persona ya tiene un usuario, solo queda desactivar su usuario', 1;
		
		set @exist = (select c.IdPersona from Contrato c where c.IdPersona = @idPersona)
		if(@exist is not null)
			THROW 51000, 'Esta persona ya tiene un contrato, no puede eliminar', 1;
		
		delete from OcupacionXPersona where IdPersona = @idPersona
		delete from Persona where IdPersona = @idPersona
		delete from PadecimientoEnfermedades where IdPersona = @idPersona
		delete from NumEmergencias where IdPersona = @idPersona
	END
GO
PRINT N'Creando Procedimiento [dbo].[DeleteTipoRedSocial]...';


GO
create procedure DeleteTipoRedSocial
	@idTipoRedSocial int, @isdelete bit out
	as
	BEGIN
		declare @count int
		set @count = (select count(rs.IdTipoRedSocial) from RedesSociales rs where rs.IdTipoRedSocial = @idTipoRedSocial)
		if(@count > 0)
			return 0
		else
		BEGIN
			delete from TipoRedSocial where IdTipoRedSocial = @idTipoRedSocial
			return 1
		END
	END
GO
PRINT N'Creando Procedimiento [dbo].[EliminarVentana]...';


GO
create procedure EliminarVentana
	@idVentana int, @idUsuario uniqueidentifier
	as
	BEGIN
		declare @exist int
		set @exist = (select count(p.IdUsuario) from Permisos p where p.IdUsuario = @idUsuario)
		if(@exist <> 0)
			THROW 51000, 'El usuario ya tiene esta ventana en sus permisos', 1;

		delete from Ventana where idVentana = @idVentana

	END
GO
PRINT N'Creando Procedimiento [dbo].[IniciarSesion]...';


GO
create procedure IniciarSesion
	@user varchar(10), @password varchar(16)
	as
	BEGIN
		select top 1 u.IdPersona from Usuario u 
			where u.usuario = @user and u.contrasena = @password
	END
GO
PRINT N'Creando Procedimiento [dbo].[InsertarVentana]...';


GO
create procedure InsertarVentana
	@Titulo varchar(100),
	@Codigo char(8)
	as 
	BEGIN
		Insert into Ventana(Titulo, Codigo) values(@Titulo, @Codigo)
	END
GO
PRINT N'Creando Procedimiento [dbo].[InsertTipoRedSocial]...';


GO
create procedure InsertTipoRedSocial
	@Tiporedsocial varchar(100)
	as
	BEGIN
		Insert into TipoRedSocial(descripcion) values(@Tiporedsocial)
	END
GO
PRINT N'Creando Procedimiento [dbo].[OcupacionPorPersona]...';


GO
create procedure OcupacionPorPersona
	@idPersona uniqueidentifier
	as 
	BEGIN
		select o.IdOcupacion, o.descripcion as 'Description' from OcupacionXPersona OXP 
		inner join Ocupacion o on o.IdOcupacion = OXP.IdOcupacion
		where OXP.IdPersona = @idPersona
	END
GO
PRINT N'Creando Procedimiento [dbo].[PersonaDatos]...';


GO
create procedure PersonaDatos
	@ID uniqueidentifier
	as
	BEGIN
		select p.IdPersona, p.Nombres,
		p.Apellidos, p.Fech_Nacimiento, p.Edad,
		p.Direccion, p.Telefono, p.Email,
		p.Identificacion, r.descripcion as 'Rol', r.code as 'CodeRol' , g.descripcion as 'GeneroDescripcion',
		g.code as 'generoCode', ti.descripcion as 'TipoIdentityDescripcion',
		ti.abreviado as 'abreviadoTipo'
		from Persona p
		inner join Genero g on g.IdGenero = p.idGenero
		inner join TipoIdentity ti on ti.IdTipoIdentity = p.idtipoIdentificacion
		inner join Rol r on r.IdRol = p.idRol
		where p.IdPersona = @ID
	END
GO
PRINT N'Creando Procedimiento [dbo].[SelectAllGenero]...';


GO
create procedure SelectAllGenero
	as
	BEGIN 
		select IdGenero, descripcion, code from Genero
	END
GO
PRINT N'Creando Procedimiento [dbo].[SelectAllPersonas]...';


GO
create procedure SelectAllPersonas
	as 
	BEGIN
		Select p.Nombres, p.Apellidos, p.Direccion, p.Edad, p.Email, 
		p.Fech_Nacimiento, p.Identificacion, p.Telefono, p.IdPersona, g.descripcion as 'Genero',
		ti.descripcion as 'TipoIdentificacion', r.descripcion as 'Rol', r.IdRol, g.IdGenero, ti.IdTipoIdentity
		from Persona p
		inner join Genero g on g.IdGenero = p.idGenero
		inner join TipoIdentity ti on ti.IdTipoIdentity = p.idtipoIdentificacion
		inner join Rol r on r.IdRol = p.idRol
	END
GO
PRINT N'Creando Procedimiento [dbo].[SelectAllRol]...';


GO
create procedure SelectAllRol
	as 
	BEGIN
		select IdRol, descripcion, code from Rol
	END
GO
PRINT N'Creando Procedimiento [dbo].[SelectAllTipoIdentificacion]...';


GO
create procedure SelectAllTipoIdentificacion
	as
	BEGIN
		select IdTipoIdentity, abreviado, descripcion from TipoIdentity
	END
GO
PRINT N'Creando Procedimiento [dbo].[SelectAllTipoRedSocial]...';


GO
create procedure SelectAllTipoRedSocial
	as
	BEGIN 
		select IdTipoRedSocial, descripcion from TipoRedSocial
	END
GO
PRINT N'Creando Procedimiento [dbo].[SelectAllVentana]...';


GO
create procedure SelectAllVentana
	as
	BEGIN
		select * from Ventana
	END
GO
PRINT N'Creando Procedimiento [dbo].[SelectOneGenero]...';


GO
create procedure SelectOneGenero
	@idGenero int
	as
	BEGIN
		select IdGenero, descripcion, code from Genero where IdGenero = @idGenero
	END
GO
PRINT N'Creando Procedimiento [dbo].[SelectOnePersona]...';


GO
create procedure SelectOnePersona
	@id uniqueidentifier
	as
	BEGIN
	Select p.Nombres, p.Apellidos, p.Direccion, p.Edad, p.Email, 
			p.Fech_Nacimiento, p.Identificacion, p.Telefono, p.IdPersona, g.descripcion as 'Genero',
			ti.descripcion as 'TipoIdentificacion', r.descripcion as 'Rol', r.IdRol, g.IdGenero, ti.IdTipoIdentity
			from Persona p
			inner join Genero g on g.IdGenero = p.idGenero
			inner join TipoIdentity ti on ti.IdTipoIdentity = p.idtipoIdentificacion
			inner join Rol r on r.IdRol = p.idRol
			where p.IdPersona = @id
END
GO
PRINT N'Creando Procedimiento [dbo].[SelectOneTipoRedSocial]...';


GO
create procedure SelectOneTipoRedSocial
	@idTipoRedSocial int
	as
	BEGIN
		select IdTipoRedSocial, descripcion from TipoRedSocial where IdTipoRedSocial = @idTipoRedSocial
	END
GO
PRINT N'Creando Procedimiento [dbo].[SelectVentanaOne]...';


GO
create procedure SelectVentanaOne
	@idVentana int
	as
	BEGIN
		select * from Ventana where idVentana = @idVentana
	END
GO
PRINT N'Creando Procedimiento [dbo].[UpdatePersonaData]...';


GO
create procedure UpdatePersonaData
		@Nombres varchar(50), @Apellidos varchar(50),
		@Fech_nac datetime, @Direccion varchar(100),
		@idRol int, @telefono varchar(10),
		@Email varchar(255), @idgenero int, 
		@Identificacion varchar(20), @idTipoIdent int, @id uniqueidentifier
	as
	Begin
		update Persona set Nombres = @Nombres,
		Apellidos = @Apellidos, Fech_Nacimiento = @Fech_nac,
		Direccion = @Direccion, idRol = @idRol,
		Telefono = @telefono, Email = @Email, idGenero = @idgenero,
		Identificacion = @Identificacion, idtipoIdentificacion = @idTipoIdent
		where IdPersona = @id
		
		exec izathfitbd.dbo.SelectOnePersona @id
		
	End
GO
PRINT N'Creando Procedimiento [dbo].[UpdateTipoRedSocial]...';


GO
create procedure UpdateTipoRedSocial
	@idTipoRedSocial int, @Tiporedsocial varchar(100)
	as
	BEGIN 
		update TipoRedSocial set descripcion = @Tiporedsocial
		where IdTipoRedSocial = @idTipoRedSocial
	END
GO
PRINT N'Creando Procedimiento [dbo].[VerificarActivoUsuario]...';


GO
create procedure VerificarActivoUsuario
@idPersona uniqueidentifier
as
BEGIN
		declare @isactive bit
		
		set @isactive = (select u.IsActivo from Usuario u where u.IdPersona = @idPersona)
		if(@isactive = 0)
			THROW 51000, 'Este usuario no se encuentra', 1;
END
GO
PRINT N'Creando Procedimiento [dbo].[VerificarEmailPersona]...';


GO
create procedure VerificarEmailPersona
	@email varchar(255)
	as
	BEGIN
		select p.IdPersona from Persona p where p.Email = @email
	END
GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Actualización completada.';


GO
