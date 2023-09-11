/*
Procedimientos Almacenados de Tabla Rol
Cree la base de datos primero
*/
go
	create procedure SelectAllRol
	as 
	BEGIN
		select IdRol, descripcion, code from Rol
	END
go