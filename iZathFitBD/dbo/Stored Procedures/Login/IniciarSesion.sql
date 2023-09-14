create procedure IniciarSesion
	@user varchar(10), @password varchar(16)
	as
	BEGIN
		declare @idpersona uniqueidentifier =
		(select top 1 u.IdPersona from Usuario u 
			where u.usuario = @user and u.contrasena = @password)
		exec VerificarActivoUsuario @idpersona

		select @idpersona
	END