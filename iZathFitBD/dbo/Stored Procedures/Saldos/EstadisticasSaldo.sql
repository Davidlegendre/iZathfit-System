CREATE PROCEDURE [dbo].[EstadisticasSaldo]
	@idpersona uniqueidentifier
AS
	declare @ContratosPagados int
	set @ContratosPagados = (select count(s.IdContrato) from Saldos s 
	inner join Contrato c on c.IdContrato = s.IdContrato
	where s.IdPersona = @idpersona and s.TotalFaltante = 0 and
	 c.IsNotValid <> 1)

	declare @ContratosAdeudados int
	set @ContratosAdeudados = (select count(s.IdContrato) from Saldos s 
	inner join Contrato c on c.IdContrato = s.IdContrato
	where s.IdPersona = @idpersona and s.TotalFaltante <> 0
	and c.IsNotValid <> 1)

	declare @CantidadAdeudadaFaltante decimal
	set @CantidadAdeudadaFaltante = (select SUM(s.TotalFaltante) from Saldos s 
	inner join Contrato c on c.IdContrato = s.IdContrato
	where s.IdPersona = @idpersona and s.TotalFaltante <> 0 and c.IsNotValid <>1)

	declare @CantidadPagada decimal
	set @CantidadPagada = (select SUM(s.TotalPagado) from Saldos s
	inner join Contrato c on c.IdContrato = s.IdContrato
	where s.IdPersona = @idpersona and c.IsNotValid <> 1)


	declare @ContratosVencidosNoPagos int 
	set @ContratosVencidosNoPagos = (select count(s.IdContrato) from Saldos s 
	inner join Contrato c on c.IdContrato = s.IdContrato
	where s.IdPersona = @idpersona and s.TotalFaltante <> 0 
	and c.IsNotValid <> 1
	and convert(date,c.FechaFinal) < convert(date, getdate()))

	select @ContratosPagados as 'ContratosPagados', @ContratosAdeudados as 'ContratosAdeudados', 
	@CantidadAdeudadaFaltante as 'CantidadAdeudadaFaltante', @CantidadPagada as 'CantidadPagada',
	@ContratosVencidosNoPagos as 'ContratosVencidosNoPagos'
RETURN 0
