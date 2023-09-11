/*
Procedimientos Almacenados de Tabla Genero
Cree la base de datos primero
*/
go
	create procedure SelectAllGenero
	as
	BEGIN 
		select IdGenero, descripcion, code from Genero
	END
go
	create procedure SelectOneGenero
	@idGenero int
	as
	BEGIN
		select IdGenero, descripcion, code from Genero where IdGenero = @idGenero
	END
go