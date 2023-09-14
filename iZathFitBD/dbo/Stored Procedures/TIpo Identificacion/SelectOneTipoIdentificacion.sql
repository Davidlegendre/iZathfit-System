CREATE PROCEDURE [dbo].[SelectOneTipoIdentificacion]
	@idtipoidentificacion int
AS
	select * from TipoIdentity ti where ti.IdTipoIdentity = @idtipoidentificacion
RETURN 0
