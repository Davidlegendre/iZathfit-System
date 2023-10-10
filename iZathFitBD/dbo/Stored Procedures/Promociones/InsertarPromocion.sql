CREATE PROCEDURE [dbo].[InsertarPromocion]
	@IdPlan int, @IdUsuario uniqueidentifier,
	@precio decimal(18,2),
	@DuracionTiempo datetime,
	@descripcion varchar(100)
AS
	insert into Promociones(IdPlan, IdUsuario,DuracionTiempo,Precio,descripcion)
	values(@IdPlan, @IdUsuario, @DuracionTiempo, @precio, @descripcion)
RETURN 0
