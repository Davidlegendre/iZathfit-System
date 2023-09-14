CREATE PROCEDURE [dbo].[DeletePlan]
	@idplan int
AS
	declare @exist int
	set @exist = (select count(IdPlan) from Contrato where IdPlan = @idplan)
	if(@exist <> 0)
		THROW 51000, 'Este Plan no puede ser eliminado ya esta en contratos', 1;

	delete from ServiciosXPLanes where idPlan = @idplan
	delete from Planes where IdPlanes = @idplan
RETURN 0
