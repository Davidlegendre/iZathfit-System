CREATE PROCEDURE [dbo].[UpdatePlanDuracion]
	@descripcion varchar(100),
@IdUsuario uniqueidentifier,
@MesesTiempo int,
@idPlanduracion int
AS
	update PlanDuracion set descripcion = @descripcion, MesesTiempo = @MesesTiempo,
	IdUsuario = @IdUsuario where IdPlanDuracion = @idPlanduracion
RETURN 0
