CREATE PROCEDURE [dbo].[SearchContratoByPersona]
	@idpersona uniqueidentifier
AS
	select c.DescripcionIsNotValid, c.Fecha_contrato, c.FechaFinal, c.FechaInicio, c.IdContrato,
	c.IdPersona, c.IdPlan, c.IdTipoPago, c.IdUsuario, c.IsNotValid, c.NumeroContrato, c.ValorOriginal, c.ValorTotal,
	concat(p.Nombres, ' ', p.Apellidos) as 'PersonaNombres', p.Identificacion, c.FechaFinalReal,
	s.TotalFaltante, pd.MesesTiempo as 'PlanDuracion'
	from Contrato c
	inner join Persona p on p.IdPersona = c.IdPersona
	inner join Saldos s on s.IdContrato = c.IdContrato
	inner join Planes pl on pl.IdPlanes = c.IdPlan
	inner join PlanDuracion pd on pd.IdPlanDuracion = pl.IdPlanDuracion
	where c.IdPersona = @idpersona and s.TotalFaltante <> 0 and c.IsNotValid = 0
RETURN 0
