CREATE PROCEDURE [dbo].[UpdatePromocion]
	@IdPlan int, @IdUsuario uniqueidentifier,
	@DescuentoPercent int,
	@DuracionTiempo datetime,
	@descripcion varchar(100), 
	@idPromocion int
AS

	

	update Promociones set IdPlan = @IdPlan,
	IdUsuario = @IdUsuario, DescuentoPercent = @DescuentoPercent,
	DuracionTiempo =@DuracionTiempo,
	descripcion = @descripcion where IdPromocion = @idPromocion
RETURN 0
