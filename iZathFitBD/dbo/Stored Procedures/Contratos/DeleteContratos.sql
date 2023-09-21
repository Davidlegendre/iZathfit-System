CREATE PROCEDURE [dbo].[DeleteContratos]
	@idcontratos uniqueidentifier
AS
	declare @isnotvalid bit
	set @isnotvalid = (select c.IsNotValid from Contrato c where c.IdContrato = @idcontratos)
	if(@isnotvalid = 0)
		THROW 51000, 'Para eliminarlo es necesario que sea no valido', 1;
	declare @idsaldos uniqueidentifier
	set @idsaldos = (select s.IdSaldos from Saldos s where s.IdContrato = @idcontratos)
	delete from SaldosXPersona where IdSaldo = @idsaldos
	delete from Saldos where IdSaldos = @idsaldos
	delete from Contrato where IdContrato = @idcontratos
RETURN 0
