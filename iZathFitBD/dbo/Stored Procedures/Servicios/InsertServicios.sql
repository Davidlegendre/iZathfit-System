CREATE PROCEDURE [dbo].[InsertServicios]
	@nombreservicio varchar(100),
	@HorarioInicio datetime,
	@HorarioFin datetime,
	@IsGrupal bit, 
	@IdUsuario uniqueidentifier
AS
	insert into Servicio(NombreServicio,HorarioInicio, HorarioFin,IsGrupal,IdUsuario)
	values(@nombreservicio, @HorarioInicio, @HorarioFin, @IsGrupal, @IdUsuario)
RETURN 0
