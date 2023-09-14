CREATE PROCEDURE [dbo].[SelectOneOcupacion]
	@idocupacion int
AS
	select o.descripcion, o.IdOcupacion from Ocupacion o where o.IdOcupacion = @idocupacion
RETURN 0
