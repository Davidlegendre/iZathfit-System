CREATE PROCEDURE [dbo].[SetContratoNotValid]
	@idcontrato uniqueidentifier,
	@descripcionIsnotvalid varchar(200),
	@isnotvalid bit
AS
	update Contrato set DescripcionIsNotValid = @descripcionIsnotvalid, IsNotValid = @isnotvalid
	where IdContrato = @idcontrato
RETURN 0
