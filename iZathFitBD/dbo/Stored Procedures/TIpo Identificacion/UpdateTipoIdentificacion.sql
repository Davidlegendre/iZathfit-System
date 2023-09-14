CREATE PROCEDURE [dbo].[UpdateTipoIdentificacion]
	@abreviado varchar(20), @descripcion varchar(50), @idtipoidentificacion int
AS
	update TipoIdentity set abreviado = @abreviado, descripcion = @descripcion
	where IdTipoIdentity = @idtipoidentificacion
	exec SelectOneTipoIdentificacion @idtipoidentificacion
RETURN 0
