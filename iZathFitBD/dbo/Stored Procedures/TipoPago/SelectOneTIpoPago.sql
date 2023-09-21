CREATE PROCEDURE [dbo].[SelectOneTIpoPago]
	@idtipopago int
AS
	select * from TipoPago where IdtipoPago = @idtipopago
RETURN 0
