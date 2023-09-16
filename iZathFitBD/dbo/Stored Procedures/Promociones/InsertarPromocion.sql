CREATE PROCEDURE [dbo].[InsertarPromocion]
	@IdPlan int, @IdUsuario uniqueidentifier,
	@DescuentoPercent int,
	@DuracionTiempo datetime,
	@descripcion varchar(100)
AS
	insert into Promociones(IdPlan, IdUsuario,DuracionTiempo,DescuentoPercent,descripcion)
	values(@IdPlan, @IdUsuario, @DuracionTiempo, @DescuentoPercent, @descripcion)
RETURN 0
