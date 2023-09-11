/*
Procedimientos Almacenados Login
Cree la base de datos primero
*/
go
	create procedure IniciarSesion
	@user varchar(10), @password varchar(16)
	as
	BEGIN
		select top 1 u.IdPersona from Usuario u 
			where u.usuario = @user and u.contrasena = @password
	END
go
create procedure VerificarActivoUsuario
@idPersona uniqueidentifier
as
BEGIN
		declare @isactive bit
		
		set @isactive = (select u.IsActivo from Usuario u where u.IdPersona = @idPersona)
		if(@isactive = 0)
			THROW 51000, 'Este usuario no se encuentra', 1;
END
go