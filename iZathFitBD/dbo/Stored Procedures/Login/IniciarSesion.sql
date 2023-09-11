create procedure IniciarSesion
	@user varchar(10), @password varchar(16)
	as
	BEGIN
		select top 1 u.IdPersona from Usuario u 
			where u.usuario = @user and u.contrasena = @password
	END