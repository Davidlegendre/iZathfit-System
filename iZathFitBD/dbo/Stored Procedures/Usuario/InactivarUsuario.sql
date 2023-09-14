CREATE PROCEDURE [dbo].[InactivarUsuario]
	@iduser uniqueidentifier
AS
	update Usuario set IsActivo = 0 where idUsuario = @iduser
RETURN 0
