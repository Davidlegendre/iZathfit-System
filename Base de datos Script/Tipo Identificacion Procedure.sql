/*
Procedimientos Almacenados de Tabla Tipo Identificacion
Cree la base de datos primero
*/
go
	create procedure SelectAllTipoIdentificacion
	as
	BEGIN
		select IdTipoIdentity, abreviado, descripcion from TipoIdentity
	END
go