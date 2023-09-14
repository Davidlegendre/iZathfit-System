CREATE PROCEDURE [dbo].[UpdateServicios]
	@nombreservicio varchar(100),
	@HorarioInicio datetime,
	@HorarioFin datetime,
	@IsGrupal bit, 
	@IdUsuario uniqueidentifier,
	@idservicios int
AS
	update Servicio set NombreServicio = @nombreservicio, HorarioInicio = @HorarioInicio,
	 HorarioFin = @HorarioFin, IsGrupal = @IsGrupal, @IdUsuario = @IdUsuario
	 where IdServicio = @idservicios
RETURN 0
