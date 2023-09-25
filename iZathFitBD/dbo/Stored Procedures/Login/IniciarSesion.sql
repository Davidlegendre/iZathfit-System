create procedure IniciarSesion
	@user varchar(10), @password varchar(16)
	as
	BEGIN
		declare @idpersona uniqueidentifier =
		(select top 1 u.IdPersona from Usuario u 
			where u.usuario = @user collate SQL_Latin1_General_CP1_CS_AS
			and u.contrasena = @password collate SQL_Latin1_General_CP1_CS_AS)
		exec VerificarActivoUsuario @idpersona

		select @idpersona
	END