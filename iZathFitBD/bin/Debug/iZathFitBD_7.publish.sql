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
USE [$(DatabaseName)];


GO
PRINT N'Quitando Clave externa restricción sin nombre en [dbo].[Persona]...';


GO
ALTER TABLE [dbo].[Persona] DROP CONSTRAINT [FK__Persona__idGener__0D7A0286];


GO
PRINT N'Quitando Clave externa restricción sin nombre en [dbo].[Persona]...';


GO
ALTER TABLE [dbo].[Persona] DROP CONSTRAINT [FK__Persona__idtipoI__0E6E26BF];


GO
PRINT N'Quitando Clave externa restricción sin nombre en [dbo].[Persona]...';


GO
ALTER TABLE [dbo].[Persona] DROP CONSTRAINT [FK__Persona__idRol__0F624AF8];


GO
PRINT N'Quitando Clave externa restricción sin nombre en [dbo].[Contrato]...';


GO
ALTER TABLE [dbo].[Contrato] DROP CONSTRAINT [FK__Contrato__IdPers__10566F31];


GO
PRINT N'Quitando Clave externa restricción sin nombre en [dbo].[NumEmergencias]...';


GO
ALTER TABLE [dbo].[NumEmergencias] DROP CONSTRAINT [FK__NumEmerge__IdPer__114A936A];


GO
PRINT N'Quitando Clave externa restricción sin nombre en [dbo].[OcupacionXPersona]...';


GO
ALTER TABLE [dbo].[OcupacionXPersona] DROP CONSTRAINT [FK__Ocupacion__IdPer__123EB7A3];


GO
PRINT N'Quitando Clave externa restricción sin nombre en [dbo].[PadecimientoEnfermedades]...';


GO
ALTER TABLE [dbo].[PadecimientoEnfermedades] DROP CONSTRAINT [FK__Padecimie__IdPer__1332DBDC];


GO
PRINT N'Quitando Clave externa restricción sin nombre en [dbo].[Usuario]...';


GO
ALTER TABLE [dbo].[Usuario] DROP CONSTRAINT [FK__Usuario__IdPerso__14270015];


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
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[Contrato]...';


GO
ALTER TABLE [dbo].[Contrato] WITH NOCHECK
    ADD FOREIGN KEY ([IdPersona]) REFERENCES [dbo].[Persona] ([IdPersona]);


GO
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[NumEmergencias]...';


GO
ALTER TABLE [dbo].[NumEmergencias] WITH NOCHECK
    ADD FOREIGN KEY ([IdPersona]) REFERENCES [dbo].[Persona] ([IdPersona]);


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
PRINT N'Creando Clave externa restricción sin nombre en [dbo].[Usuario]...';


GO
ALTER TABLE [dbo].[Usuario] WITH NOCHECK
    ADD FOREIGN KEY ([IdPersona]) REFERENCES [dbo].[Persona] ([IdPersona]);


GO
PRINT N'Modificando Procedimiento [dbo].[AgregarPersona]...';


GO
ALTER procedure AgregarPersona
		@Nombres varchar(50), @Apellidos varchar(50),
		@Fech_nac datetime, @Direccion varchar(100),
		@idrol int, @telefono char(9),
		@Email varchar(255), @idgenero int, 
		@Identificacion varchar(20), @idTipoIdent int
	as
	BEGIN
		declare @uid uniqueidentifier
		set @uid = newid()
		declare @exist int
		set @exist = (select count(p.Identificacion) from Persona p where p.Identificacion = @Identificacion)
		if(@exist <> 0)
			THROW 51000, 'Esta indentificacion ya existe en la base de datos', 1;

		insert into Persona(IdPersona, 
		Nombres, Apellidos, Fech_Nacimiento, Direccion, 
		idRol, Telefono, Email,idGenero,
		Identificacion,idtipoIdentificacion)
		values(@uid,@Nombres,@Apellidos,@Fech_nac,@Direccion,
		@idrol, @telefono,@Email,@idgenero,@Identificacion,@idTipoIdent)

		exec izathfitbd.dbo.SelectOnePersona @uid
	END
GO
PRINT N'Actualizando Procedimiento [dbo].[DeletePersona]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[DeletePersona]';


GO
PRINT N'Actualizando Procedimiento [dbo].[PersonaDatos]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PersonaDatos]';


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
        WHERE  [parent_object_id] IN (OBJECT_ID(N'dbo.Persona'), OBJECT_ID(N'dbo.Contrato'), OBJECT_ID(N'dbo.NumEmergencias'), OBJECT_ID(N'dbo.OcupacionXPersona'), OBJECT_ID(N'dbo.PadecimientoEnfermedades'), OBJECT_ID(N'dbo.Usuario'))
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
