CREATE PROCEDURE [dbo].[SelectOnePlan]
@idplan int
AS
	select p.descripcion, p.Precio, pd.MesesTiempo, pd.descripcion as 'PlanDuracionDescrip',
	p.IdPlanDuracion, p.IdUsuario, p.IdPlanes	
	from Planes p
	inner join PlanDuracion pd on pd.IdPlanDuracion = p.IdPlanDuracion
	where p.IdPlanes = @idplan
RETURN 0
