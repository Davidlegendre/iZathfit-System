CREATE PROCEDURE [dbo].[UpdatePersonaDatosPerfil]
	@nombre varchar(50),
	@apellido varchar(50),
	@fechnacimiento datetime,
	@direccion varchar(100),
	@telefono varchar(9),
	@email varchar(255),
	@idgenero int,
	@idpersona uniqueidentifier
AS
	update Persona set Nombres = @nombre, Apellidos = @apellido,
	Fech_Nacimiento = @fechnacimiento, Direccion = @direccion, 
	Telefono = @telefono, Email = @email, idGenero = @idgenero
	where IdPersona = @idpersona

RETURN 0
