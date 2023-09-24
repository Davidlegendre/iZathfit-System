CREATE PROCEDURE [dbo].[UpdateContrato]
	@idcontrato uniqueidentifier,
	@fechafinal datetime,
	@numcontrato char(6)
AS
	declare @isexist int
	set @isexist = (select count(*) from Contrato where NumeroContrato = @numcontrato and IdContrato <> @idcontrato)
	--si el numero de contrato existe y no es el mio, entonces error
	--solo si es mio no importa
	if(@isexist <> 0)
		THROW 51000, 'Ya existe un contrato con este numero', 1;

	update Contrato set FechaFinal = @fechafinal, NumeroContrato = @numcontrato
	where IdContrato = @idcontrato
RETURN 0
