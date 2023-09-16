CREATE PROCEDURE [dbo].[DeletePromocion]
	@idPromocion int
AS
	delete from Promociones where IdPromocion = @idPromocion
RETURN 0
