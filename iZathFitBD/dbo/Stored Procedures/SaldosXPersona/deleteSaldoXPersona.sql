CREATE PROCEDURE [dbo].[deleteSaldoXPersona]
	@IdPositionRow int,
	@idContrato uniqueidentifier,
	@IdSaldo uniqueidentifier
AS
	declare @exist int
	set @exist = (select count(*) from SaldosXPersona s where s.IdSaldo = @IdSaldo and s.IdContrato = @idContrato)
	if(@exist <> 0)
	begin

		declare @totalanterior int
		set @totalanterior = (select s.TotalPagadoActual from SaldosXPersona s where s.IdSaldo = @IdSaldo and s.IdContrato = @idContrato 
		and s.IdPositionRow = @IdPositionRow)

		delete from SaldosXPersona where IdContrato = @idContrato and IdSaldo = @IdSaldo and IdPositionRow = @IdPositionRow
		update Saldos set TotalPagado = TotalPagado - @totalanterior where IdSaldos = @IdSaldo and IdContrato = @idContrato
	end

RETURN 0
