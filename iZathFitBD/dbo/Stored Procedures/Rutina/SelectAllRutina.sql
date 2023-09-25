CREATE PROCEDURE [dbo].[SelectAllRutina]
@fecha datetime
AS
	select r.MontoPago, r.FechaPagoRutina, r.IdRutina, r.IdTipoPago, tp.descripcion as 'TipoPago' from Rutina r
	inner join TipoPago tp on tp.IdtipoPago = r.IdTipoPago
	where convert(date, r.FechaPagoRutina) = convert(date, @fecha)
RETURN 0
