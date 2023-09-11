/*
Procedimientos Almacenados de Tabla Ventana
Cree la base de datos primero
*/
go
	create procedure InsertarVentana
	@Titulo varchar(100),
	@Codigo char(8)
	as 
	BEGIN
		Insert into Ventana(Titulo, Codigo) values(@Titulo, @Codigo)
	END
go
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
go
	create procedure ActualizarVentana
	@Titulo varchar(100),
	@Codigo char(8), @idVentana int
	as
	BEGIN
		update Ventana set Titulo = @Titulo, Codigo = @Codigo 
		where idVentana = @idVentana
	END
go
	create procedure SelectAllVentana
	as
	BEGIN
		select * from Ventana
	END
go
	create procedure SelectVentanaOne
	@idVentana int
	as
	BEGIN
		select * from Ventana where idVentana = @idVentana
	END
go