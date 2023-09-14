CREATE PROCEDURE [dbo].[DeletePadecimientos]
	@idPersona uniqueidentifier
AS
	delete from PadecimientoEnfermedades where IdPersona = @idPersona
RETURN 0
