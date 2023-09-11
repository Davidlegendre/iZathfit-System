create procedure InsertarVentana
	@Titulo varchar(100),
	@Codigo char(8)
	as 
	BEGIN
		Insert into Ventana(Titulo, Codigo) values(@Titulo, @Codigo)
	END