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
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET PAGE_VERIFY NONE,
                DISABLE_BROKER 
            WITH ROLLBACK IMMEDIATE;
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
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
PRINT N'Quitando Clave externa restricción sin nombre en [dbo].[Persona]...';


GO
ALTER TABLE [dbo].[Persona] DROP CONSTRAINT [FK__Persona__idGener__35BCFE0A];


GO
PRINT N'Quitando Clave externa restricción sin nombre en [dbo].[Persona]...';


GO
ALTER TABLE [dbo].[Persona] DROP CONSTRAINT [FK__Persona__idtipoI__36B12243];


GO
PRINT N'Quitando Clave externa restricción sin nombre en [dbo].[Persona]...';


GO
ALTER TABLE [dbo].[Persona] DROP CONSTRAINT [FK__Persona__idRol__37A5467C];


GO
PRINT N'Quitando Clave externa restricción sin nombre en [dbo].[OcupacionXPersona]...';


GO
ALTER TABLE [dbo].[OcupacionXPersona] DROP CONSTRAINT [FK__Ocupacion__IdPer__3D5E1FD2];


GO
PRINT N'Quitando Clave externa restricción sin nombre en [dbo].[PadecimientoEnfermedades]...';


GO
ALTER TABLE [dbo].[PadecimientoEnfermedades] DROP CONSTRAINT [FK__Padecimie__IdPer__403A8C7D];


GO
PRINT N'Quitando Clave externa restricción sin nombre en [dbo].[NumEmergencias]...';


GO
ALTER TABLE [dbo].[NumEmergencias] DROP CONSTRAINT [FK__NumEmerge__IdPer__4222D4EF];


GO
PRINT N'Quitando Clave externa restricción sin nombre en [dbo].[Usuario]...';


GO
ALTER TABLE [dbo].[Usuario] DROP CONSTRAINT [FK__Usuario__IdPerso__47DBAE45];


GO
PRINT N'Quitando Clave externa restricción sin nombre en [dbo].[Contrato]...';


GO
ALTER TABLE [dbo].[Contrato] DROP CONSTRAINT [FK__Contrato__IdPers__6477ECF3];


GO
PRINT N'Iniciando recompilación de la tabla [dbo].[Persona]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Persona] (
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
    UNIQUE NONCLUSTERED ([Identificacion] ASC),
    PRIMARY KEY CLUSTERED ([IdPersona] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Persona])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_Persona] ([IdPersona], [Nombres], [Apellidos], [Fech_Nacimiento], [Direccion], [Telefono], [Email], [idGenero], [Identificacion], [idtipoIdentificacion], [idRol])
        SELECT   [IdPersona],
                 [Nombres],
                 [Apellidos],
                 [Fech_Nacimiento],
                 [Direccion],
                 [Telefono],
                 [Email],
                 [idGenero],
                 [Identificacion],
                 [idtipoIdentificacion],
                 [idRol]
        FROM     [dbo].[Persona]
        ORDER BY [IdPersona] ASC;
    END

DROP TABLE [dbo].[Persona];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Persona]', N'Persona';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[Persona]...';


GO
ALTER TABLE [dbo].[Persona] WITH NOCHECK
    ADD FOREIGN KEY ([idGenero]) REFERENCES [dbo].[Genero] ([IdGenero]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[Persona]...';


GO
ALTER TABLE [dbo].[Persona] WITH NOCHECK
    ADD FOREIGN KEY ([idtipoIdentificacion]) REFERENCES [dbo].[TipoIdentity] ([IdTipoIdentity]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[Persona]...';


GO
ALTER TABLE [dbo].[Persona] WITH NOCHECK
    ADD FOREIGN KEY ([idRol]) REFERENCES [dbo].[Rol] ([IdRol]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[OcupacionXPersona]...';


GO
ALTER TABLE [dbo].[OcupacionXPersona] WITH NOCHECK
    ADD FOREIGN KEY ([IdPersona]) REFERENCES [dbo].[Persona] ([IdPersona]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[PadecimientoEnfermedades]...';


GO
ALTER TABLE [dbo].[PadecimientoEnfermedades] WITH NOCHECK
    ADD FOREIGN KEY ([IdPersona]) REFERENCES [dbo].[Persona] ([IdPersona]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[NumEmergencias]...';


GO
ALTER TABLE [dbo].[NumEmergencias] WITH NOCHECK
    ADD FOREIGN KEY ([IdPersona]) REFERENCES [dbo].[Persona] ([IdPersona]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[Usuario]...';


GO
ALTER TABLE [dbo].[Usuario] WITH NOCHECK
    ADD FOREIGN KEY ([IdPersona]) REFERENCES [dbo].[Persona] ([IdPersona]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[Contrato]...';


GO
ALTER TABLE [dbo].[Contrato] WITH NOCHECK
    ADD FOREIGN KEY ([IdPersona]) REFERENCES [dbo].[Persona] ([IdPersona]);


GO
PRINT N'Modificando Procedimiento [dbo].[DeletePersona]...';


GO
ALTER procedure DeletePersona
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
PRINT N'Modificando Procedimiento [dbo].[PersonaDatos]...';


GO
ALTER procedure PersonaDatos
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
PRINT N'Modificando Procedimiento [dbo].[ChangePasswordUser]...';


GO
ALTER procedure ChangePasswordUser
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
PRINT N'Modificando Procedimiento [dbo].[IniciarSesion]...';


GO
ALTER procedure IniciarSesion
	@user varchar(10), @password varchar(16)
	as
	BEGIN
		select top 1 u.IdPersona from Usuario u 
			where u.usuario = @user and u.contrasena = @password
	END
GO
PRINT N'Modificando Procedimiento [dbo].[InsertarVentana]...';


GO
ALTER procedure InsertarVentana
	@Titulo varchar(100),
	@Codigo char(8)
	as 
	BEGIN
		Insert into Ventana(Titulo, Codigo) values(@Titulo, @Codigo)
	END
GO
PRINT N'Modificando Procedimiento [dbo].[OcupacionPorPersona]...';


GO
ALTER procedure OcupacionPorPersona
	@idPersona uniqueidentifier
	as 
	BEGIN
		select o.IdOcupacion, o.descripcion as 'Description' from OcupacionXPersona OXP 
		inner join Ocupacion o on o.IdOcupacion = OXP.IdOcupacion
		where OXP.IdPersona = @idPersona
	END
GO
PRINT N'Modificando Procedimiento [dbo].[SelectAllRol]...';


GO
ALTER procedure SelectAllRol
	as 
	BEGIN
		select IdRol, descripcion, code from Rol
	END
GO
PRINT N'Modificando Procedimiento [dbo].[SelectAllTipoIdentificacion]...';


GO
ALTER procedure SelectAllTipoIdentificacion
	as
	BEGIN
		select IdTipoIdentity, abreviado, descripcion from TipoIdentity
	END
GO
PRINT N'Actualizando Procedimiento [dbo].[AgregarPersona]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[AgregarPersona]';


GO
PRINT N'Actualizando Procedimiento [dbo].[SelectAllPersonas]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[SelectAllPersonas]';


GO
PRINT N'Actualizando Procedimiento [dbo].[SelectOnePersona]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[SelectOnePersona]';


GO
PRINT N'Actualizando Procedimiento [dbo].[UpdatePersonaData]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[UpdatePersonaData]';


GO
PRINT N'Actualizando Procedimiento [dbo].[VerificarEmailPersona]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[VerificarEmailPersona]';


GO
PRINT N'Comprobando los datos existentes con las restricciones recién creadas';


GO
USE [$(DatabaseName)];


GO
CREATE TABLE [#__checkStatus] (
    id           INT            IDENTITY (1, 1) PRIMARY KEY CLUSTERED,
    [Schema]     NVARCHAR (256),
    [Table]      NVARCHAR (256),
    [Constraint] NVARCHAR (256)
);

SET NOCOUNT ON;

DECLARE tableconstraintnames CURSOR LOCAL FORWARD_ONLY
    FOR SELECT SCHEMA_NAME([schema_id]),
               OBJECT_NAME([parent_object_id]),
               [name],
               0
        FROM   [sys].[objects]
        WHERE  [parent_object_id] IN (OBJECT_ID(N'dbo.Persona'), OBJECT_ID(N'dbo.OcupacionXPersona'), OBJECT_ID(N'dbo.PadecimientoEnfermedades'), OBJECT_ID(N'dbo.NumEmergencias'), OBJECT_ID(N'dbo.Usuario'), OBJECT_ID(N'dbo.Contrato'))
               AND [type] IN (N'F', N'C')
                   AND [object_id] IN (SELECT [object_id]
                                       FROM   [sys].[check_constraints]
                                       WHERE  [is_not_trusted] <> 0
                                              AND [is_disabled] = 0
                                       UNION
                                       SELECT [object_id]
                                       FROM   [sys].[foreign_keys]
                                       WHERE  [is_not_trusted] <> 0
                                              AND [is_disabled] = 0);

DECLARE @schemaname AS NVARCHAR (256);

DECLARE @tablename AS NVARCHAR (256);

DECLARE @checkname AS NVARCHAR (256);

DECLARE @is_not_trusted AS INT;

DECLARE @statement AS NVARCHAR (1024);

BEGIN TRY
    OPEN tableconstraintnames;
    FETCH tableconstraintnames INTO @schemaname, @tablename, @checkname, @is_not_trusted;
    WHILE @@fetch_status = 0
        BEGIN
            PRINT N'Comprobando restricción:' + @checkname + N' [' + @schemaname + N'].[' + @tablename + N']';
            SET @statement = N'ALTER TABLE [' + @schemaname + N'].[' + @tablename + N'] WITH ' + CASE @is_not_trusted WHEN 0 THEN N'CHECK' ELSE N'NOCHECK' END + N' CHECK CONSTRAINT [' + @checkname + N']';
            BEGIN TRY
                EXECUTE [sp_executesql] @statement;
            END TRY
            BEGIN CATCH
                INSERT  [#__checkStatus] ([Schema], [Table], [Constraint])
                VALUES                  (@schemaname, @tablename, @checkname);
            END CATCH
            FETCH tableconstraintnames INTO @schemaname, @tablename, @checkname, @is_not_trusted;
        END
END TRY
BEGIN CATCH
    PRINT ERROR_MESSAGE();
END CATCH

IF CURSOR_STATUS(N'LOCAL', N'tableconstraintnames') >= 0
    CLOSE tableconstraintnames;

IF CURSOR_STATUS(N'LOCAL', N'tableconstraintnames') = -1
    DEALLOCATE tableconstraintnames;

SELECT N'Error de comprobación de restricción:' + [Schema] + N'.' + [Table] + N',' + [Constraint]
FROM   [#__checkStatus];

IF @@ROWCOUNT > 0
    BEGIN
        DROP TABLE [#__checkStatus];
        RAISERROR (N'Error al comprobar las restricciones', 16, 127);
    END

SET NOCOUNT OFF;

DROP TABLE [#__checkStatus];


GO
PRINT N'Actualización completada.';


GO
