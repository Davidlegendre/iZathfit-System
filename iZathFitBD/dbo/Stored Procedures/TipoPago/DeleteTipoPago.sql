CREATE PROCEDURE [dbo].[DeleteTipoPago]
	@idtipopago int
AS
	declare @exist int
	set @exist = (select count(c.IdTipoPago) from Contrato c where IdTipoPago = @idtipopago)
	if(@exist <> 0)
		THROW 51000, 'Este Tipo de Pago no se puede eliminar porque ya esta siendo usado', 1;

	delete from TipoPago where IdtipoPago = @idtipopago

RETURN 0
