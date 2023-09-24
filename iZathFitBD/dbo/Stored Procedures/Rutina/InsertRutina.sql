CREATE PROCEDURE [dbo].[InsertRutina]
	@pago decimal(18,2),
	@idtipopago int
AS
declare @uid uniqueidentifier
set @uid = newid()
	insert into Rutina(IdRutina,IdTipoPago, MontoPago) values(@uid,@idtipopago, @pago)
RETURN 0
