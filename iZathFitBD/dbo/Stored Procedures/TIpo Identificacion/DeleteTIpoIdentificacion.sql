CREATE PROCEDURE [dbo].[DeleteTIpoIdentificacion]
	@idtipoidentificacion int
AS
	declare @exist int
	set @exist = (select count(p.idtipoIdentificacion) from Persona p where p.idtipoIdentificacion = @idtipoidentificacion)
	if(@exist > 0)
		THROW 51000, 'No se puede eliminar el tipo de identificacion ya que esta siendo usado por alguna persona', 1;
	delete from TipoIdentity where IdTipoIdentity = @idtipoidentificacion
RETURN 0
