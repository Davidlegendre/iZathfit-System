CREATE PROCEDURE [dbo].[DeleteRutina]
	@idrutina uniqueidentifier
AS
	delete from Rutina where IdRutina = @idrutina
RETURN 0
