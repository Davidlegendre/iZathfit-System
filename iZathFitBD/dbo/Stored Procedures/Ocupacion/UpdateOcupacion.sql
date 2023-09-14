CREATE PROCEDURE [dbo].[UpdateOcupacion]
	@descripcion varchar(100), @idocupacion int
AS
	update Ocupacion set descripcion = @descripcion where IdOcupacion = @idocupacion
	exec dbo.SelectOneOcupacion @idocupacion
RETURN 0
