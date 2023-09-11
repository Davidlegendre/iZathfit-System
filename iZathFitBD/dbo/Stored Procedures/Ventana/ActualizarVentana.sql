create procedure ActualizarVentana
	@Titulo varchar(100),
	@Codigo char(8), @idVentana int
	as
	BEGIN
		update Ventana set Titulo = @Titulo, Codigo = @Codigo 
		where idVentana = @idVentana
	END