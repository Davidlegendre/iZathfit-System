CREATE PROCEDURE [dbo].[insertSaldoXPersona]
	@IdPersona uniqueidentifier ,
	@IdContrato uniqueidentifier,
	@TotalPagadoActual decimal(18,2),
	@idtipopago int
AS
	declare @IdSaldo uniqueidentifier
	set @IdSaldo = (select IdSaldos from Saldos where IdContrato = @IdContrato and IdPersona = @IdPersona)

	if(@IdSaldo is null)
		THROW 51000, 'Este Contrato no Coincide con la Persona', 1;

	declare @ispagado decimal
	set @ispagado = (select TotalFaltante from Saldos where IdSaldos = @IdSaldo)
	if(@ispagado = 0)
		THROW 51000, 'No se puede añadir mas ya esta todo pagado para este contrato', 1;

	if(@TotalPagadoActual > @ispagado)
		THROW 51000, 'No se puede añadir mas, tiene que ser menor al total que falta', 1;

	declare @contiguo int
	set @contiguo = (select count(*) from SaldosXPersona s where s.IdContrato = @IdContrato and s.IdSaldo = s.IdSaldo)
	set @contiguo = @contiguo + 1

	insert into SaldosXPersona(IdContrato, IdPersona,IdSaldo, TotalPagadoActual, IdPositionRow, IdTipoPago)
	values(@IdContrato, @IdPersona, @IdSaldo, @TotalPagadoActual, @contiguo, @idtipopago)

	update Saldos set TotalPagado = TotalPagado + @TotalPagadoActual
	where IdSaldos = @IdSaldo
RETURN 0
