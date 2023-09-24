CREATE PROCEDURE [dbo].[SumRutinasByDate]
	@date datetime
AS
	select (select 'Rutina') as 'Nombres', sum(MontoPago) as 'Total' from Rutina r
	where convert(date, r.FechaPagoRutina) = convert(Date, @date)
RETURN 0
