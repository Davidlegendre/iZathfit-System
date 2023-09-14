CREATE PROCEDURE [dbo].[DeleteOcupacion]
	@idocupacion int
AS
	declare @exist int
	set @exist = (select top 1 p.idOcupacion from Persona p where p.idOcupacion = @idocupacion)
	if(@exist is not null)
		THROW 51000, 'La Ocupacion ya esta siendo utilizada, cambielo en las personas', 1;

	delete from Ocupacion where IdOcupacion = @idocupacion
RETURN 0
