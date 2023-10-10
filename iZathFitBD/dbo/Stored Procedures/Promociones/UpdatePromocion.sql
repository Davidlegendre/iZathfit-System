CREATE PROCEDURE [dbo].[UpdatePromocion]
	@IdPlan int, @IdUsuario uniqueidentifier,
	@precio decimal(18,2),
	@DuracionTiempo datetime,
	@descripcion varchar(100), 
	@idPromocion int
AS

	

	update Promociones set IdPlan = @IdPlan,
	IdUsuario = @IdUsuario, Precio = @precio,
	DuracionTiempo =@DuracionTiempo,
	descripcion = @descripcion where IdPromocion = @idPromocion
RETURN 0
