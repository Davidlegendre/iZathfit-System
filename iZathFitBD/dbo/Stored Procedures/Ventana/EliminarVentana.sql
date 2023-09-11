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