CREATE PROCEDURE [dbo].[InsertContratos]
	@IdPlan int,
	@IdPersona uniqueidentifier,
	@IdUsuario uniqueidentifier,
	@ValorTotal decimal(18,2),
	@Descuento int,
	@ValorOriginal decimal(18,2),
	@FechaFinal datetime,
	@NumeroContrato char(6),
	@IdTipoPago int
AS

declare @isexist int
set @isexist = (select count(*) from Contrato where NumeroContrato = @NumeroContrato)
if(@isexist <> 0)
	THROW 51000, 'El numero de contrato no esta disponible', 1;

declare @uid uniqueidentifier
set @uid = NEWID()
	insert into Contrato(IdContrato,IdPlan,IdPersona,IdUsuario,ValorTotal,Descuento,FechaFinal,NumeroContrato,IdTipoPago, ValorOriginal)
	values(@uid,@IdPlan, @IdPersona, @IdUsuario, @ValorTotal, @Descuento, @FechaFinal, @NumeroContrato, @IdTipoPago, @ValorOriginal)
	declare @uidsaldos uniqueidentifier = newid()
	insert into Saldos(IdContrato,IdPersona,IdSaldos, ValorTotal) values(@uid, @IdPersona, @uidsaldos, @ValorTotal)
RETURN 0
