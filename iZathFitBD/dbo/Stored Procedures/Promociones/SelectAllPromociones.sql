CREATE PROCEDURE [dbo].[SelectAllPromociones]
AS
	
	select pr.IdPromocion, pr.IdPlan, pr.IdUsuario, pr.IsActivo,
	pr.DuracionTiempo, pr.DescuentoPercent, pr.descripcion,
	pd.MesesTiempo, p.Precio
	from Promociones pr
	inner join Planes p on p.IdPlanes = pr.IdPlan
	inner join PlanDuracion pd on pd.IdPlanDuracion = p.IdPlanDuracion
RETURN 0