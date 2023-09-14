CREATE PROCEDURE [dbo].[SelectOneServicioXPlanes]
	@idplan int
AS
	select s.NombreServicio, s.IsGrupal, s.HorarioInicio, s.HorarioFin,s.IdServicio, s.IdUsuario
	from ServiciosXPLanes sxp
	inner join Servicio s on s.IdServicio = sxp.idServicio
	where sxp.idPlan = @idplan
RETURN 0
