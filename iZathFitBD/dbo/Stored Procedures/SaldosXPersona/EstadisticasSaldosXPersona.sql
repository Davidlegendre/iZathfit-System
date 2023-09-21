CREATE PROCEDURE [dbo].[EstadisticasSaldosXPersona]
	@idpersona uniqueidentifier
AS
	declare @UltimaFechadePago datetime
	declare @UltimoContratoPagado char(6)

	set @UltimaFechadePago = (select top 1 sp.FechaPago from SaldosXPersona sp 
	inner join Contrato c on c.IdContrato = sp.IdContrato
	where sp.IdPersona = @idpersona 
	and c.IsNotValid <> 1
	order by sp.FechaPago desc)

	set @UltimoContratoPagado = (select top 1 c.NumeroContrato from SaldosXPersona sp 
	inner join Contrato c on c.IdContrato = sp.IdContrato
	where sp.IdPersona = @idpersona and c.IsNotValid <> 1
	order by sp.FechaPago desc)

	select @UltimaFechadePago as 'UltimaFechadePago', @UltimoContratoPagado as 'UltimoContratoPagado'
RETURN 0
