CREATE PROCEDURE [dbo].[SelectOneContratoPerDNIPerson]
	@Identificacion varchar(20)
AS
	select c.DescripcionIsNotValid, c.Descuento, c.Fecha_contrato, c.FechaFinal, c.FechaInicio, c.IdContrato,
	c.IdPersona, c.IdPlan, c.IdTipoPago, c.IdUsuario, c.IsNotValid, c.NumeroContrato, c.ValorOriginal, c.ValorTotal,
	ti.descripcion as 'TipoPago', p.Precio as 'PrecioPlan',
	concat(pe.Nombres, ' ', pe.Apellidos) as 'PersonaNombres', pe.Identificacion, c.FechaFinalReal
	from Contrato c
	inner join TipoPago ti on ti.IdtipoPago = c.IdTipoPago
	inner join Planes p on p.IdPlanes = c.IdPlan
	inner join Persona pe on pe.IdPersona = c.IdPersona
	where pe.Identificacion = @Identificacion and c.IsNotValid <> 1
RETURN 0
