CREATE PROCEDURE [dbo].[InsertPlanDuracion]
@descripcion varchar(100),
@IdUsuario uniqueidentifier,
@MesesTiempo int
AS
	insert into PlanDuracion(IdUsuario, descripcion,MesesTiempo)
	values(@IdUsuario, @descripcion, @MesesTiempo)
RETURN 0
