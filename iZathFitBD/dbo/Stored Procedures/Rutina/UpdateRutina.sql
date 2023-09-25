CREATE PROCEDURE [dbo].[UpdateRutina]
	@pago decimal(18,2),
	@idtipopago int,
	@idRutina uniqueidentifier
AS
	update Rutina set MontoPago = @pago, IdTipoPago = @idtipopago
	where IdRutina = @idRutina
RETURN 0
