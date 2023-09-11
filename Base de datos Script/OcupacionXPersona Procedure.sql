/*
Procedimientos Almacenados de Tabla OcupacionXPersona
Cree la base de datos primero
*/
go
	create procedure OcupacionPorPersona
	@idPersona uniqueidentifier
	as 
	BEGIN
		select o.IdOcupacion, o.descripcion as 'Description' from OcupacionXPersona OXP 
		inner join Ocupacion o on o.IdOcupacion = OXP.IdOcupacion
		where OXP.IdPersona = @idPersona
	END
go