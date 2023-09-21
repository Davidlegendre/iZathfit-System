CREATE PROCEDURE [dbo].[SelectAllContratos]
AS
	select c.DescripcionIsNotValid, c.Descuento, c.Fecha_contrato, c.FechaFinal, c.FechaInicio, c.IdContrato,
	c.IdPersona, c.IdPlan, c.IdTipoPago, c.IdUsuario, c.IsNotValid, c.NumeroContrato, c.ValorOriginal, c.ValorTotal,
	concat(p.Nombres, ' ', p.Apellidos) as 'PersonaNombres', p.Identificacion
	from Contrato c
	inner join Persona p on p.IdPersona = c.IdPersona
RETURN 0
