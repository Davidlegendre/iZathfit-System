CREATE PROCEDURE [dbo].[UpdateTipoPago]
	@descripcion varchar(100),
	@idtipopago int
AS
	update TipoPago set descripcion = @descripcion where IdtipoPago = @idtipopago
RETURN 0
