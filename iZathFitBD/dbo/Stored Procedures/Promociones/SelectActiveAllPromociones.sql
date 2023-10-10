CREATE PROCEDURE [dbo].[SelectActiveAllPromociones]
AS
	
	select pr.IdPromocion, pr.IdPlan, pr.IdUsuario, pr.IsActivo,
	pr.DuracionTiempo, pr.Precio as 'PromoPrecio', pr.descripcion,
	pd.MesesTiempo, p.Precio
	from Promociones pr
	inner join Planes p on p.IdPlanes = pr.IdPlan
	inner join PlanDuracion pd on pd.IdPlanDuracion = p.IdPlanDuracion
	where pr.IsActivo = 1
RETURN 0
