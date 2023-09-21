CREATE PROCEDURE [dbo].[SelectAllSaldoXPersona]
	
AS
	select sxp.IdContrato, sxp.IdPersona, sxp.IdSaldo, sxp.TotalPagadoActual, 
	concat(p.Nombres, ' ', p.Apellidos) as 'NombrePersona', p.Identificacion,
	sxp.IdPositionRow, sxp.IdTipoPago,
	c.NumeroContrato, c.ValorTotal as 'totalContrato',
	s.TotalFaltante, s.TotalPagado, tp.descripcion as 'TipoPago',
	c.IsNotValid, sxp.FechaPago
	from SaldosXPersona sxp
	inner join Persona p on p.IdPersona = sxp.IdPersona
	inner join Contrato c on c.IdContrato = sxp.IdContrato
	inner join Saldos s on s.IdSaldos = sxp.IdSaldo
	inner join TipoPago tp on tp.IdtipoPago = sxp.IdTipoPago
RETURN 0
