CREATE PROCEDURE [dbo].[DeleteServicio]
	@idservicio int
AS
	declare @exist int
	set @exist = (select count(p.idPlan) from ServiciosXPLanes p where p.idServicio = @idservicio)
	if(@exist <> 0)
		THROW 51000, 'Este servicio no se puede eliminar ya que ya esta siendo usado', 1;

	delete from Servicio where IdServicio = @idservicio
RETURN 0
