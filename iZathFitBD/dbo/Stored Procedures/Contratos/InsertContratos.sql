CREATE PROCEDURE [dbo].[InsertContratos]
	@IdPlan int,
	@IdPersona uniqueidentifier,
	@IdUsuario uniqueidentifier,
	@ValorTotal decimal(18,2),
	@ValorOriginal decimal(18,2),
	@FechaFinal datetime,
	@Fechafinalreal datetime,
	@NumeroContrato char(6),
	@FechInicial datetime,
	@IdTipoPago int,
	@idcontrato uniqueidentifier output
AS

declare @isexist int
set @isexist = (select count(*) from Contrato where NumeroContrato = @NumeroContrato)
if(@isexist <> 0)
	THROW 51000, 'El numero de contrato no esta disponible', 1;

declare @uid uniqueidentifier
set @uid = NEWID()
	insert into Contrato(IdContrato,IdPlan,IdPersona,IdUsuario,ValorTotal,FechaFinal, FechaFinalReal,NumeroContrato,IdTipoPago, ValorOriginal, FechaInicio)
	values(@uid,@IdPlan, @IdPersona, @IdUsuario, @ValorTotal, @FechaFinal, @Fechafinalreal, @NumeroContrato, @IdTipoPago, @ValorOriginal, @FechInicial)
	declare @uidsaldos uniqueidentifier = newid()
	insert into Saldos(IdContrato,IdPersona,IdSaldos, ValorTotal) values(@uid, @IdPersona, @uidsaldos, @ValorTotal)
	set @idcontrato = @uid
RETURN 0
