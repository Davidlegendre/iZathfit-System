create procedure SelectVentanaOne
	@idVentana int
	as
	BEGIN
		select * from Ventana where idVentana = @idVentana
	END