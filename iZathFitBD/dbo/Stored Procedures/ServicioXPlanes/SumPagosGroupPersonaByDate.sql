CREATE PROCEDURE [dbo].[SumPagosGroupPersonaByDate]
	@date datetime
AS
	select CONCAT(p.Nombres, ' ', p.Apellidos) as 'Nombres', Sum(sp.TotalPagadoActual) as 'Total' from SaldosXPersona sp 
	inner join Persona p on p.IdPersona = sp.IdPersona
	where convert(date, sp.FechaPago) = convert(Date, @date)
	group by CONCAT(p.Nombres, ' ', p.Apellidos)
RETURN 0
