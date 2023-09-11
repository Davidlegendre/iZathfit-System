/*
Procedimientos Almacenados de Tabla Usuario
Cree la base de datos primero
*/
go
	create procedure ChangePasswordUser
	@password varchar(16), @idpersona uniqueidentifier
	AS
	BEGIN
		declare @isequals varchar(16)
		set @isequals = (select u.contrasena from Usuario u where u.IdPersona = @idpersona)
		if(@isequals = @password)
			THROW 51000, 'La contraseņa es la misma', 1;
		else
		BEGIN
			update Usuario set contrasena = @password where IdPersona = @idpersona
		END
	END
go