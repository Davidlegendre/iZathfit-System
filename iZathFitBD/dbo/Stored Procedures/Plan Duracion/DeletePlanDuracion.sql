CREATE PROCEDURE [dbo].[DeletePlanDuracion]
	@idplanduracion int
AS
	declare @exist int
	set @exist = (select count(p.IdPlanes) from Planes p where p.IdPlanDuracion = @idplanduracion)
	if(@exist <> 0)
		THROW 51000, 'Este plan de duracion no puede ser eliminado ya que esta siendo usado en planes', 1;

	delete from PlanDuracion where IdPlanDuracion = @idplanduracion

RETURN 0
