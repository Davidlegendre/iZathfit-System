CREATE PROCEDURE [dbo].[UpdatePlanes]
	@ServiciosType ServiciosType readonly,
	@idplanduracion int,
	@idusuario uniqueidentifier,
	@precio decimal(18,2),
	@descripcion varchar(100),
	@idplan int
AS
	delete from ServiciosXPLanes where idPlan = @idplan

	update Planes set descripcion = @descripcion, Precio = @precio,
	IdUsuario =@idusuario, IdPlanDuracion = @idplanduracion
	where IdPlanes = @idplan

	insert into ServiciosXPLanes(idPlan, idServicio)
	select @idplan, idServicio from @ServiciosType

RETURN 0
