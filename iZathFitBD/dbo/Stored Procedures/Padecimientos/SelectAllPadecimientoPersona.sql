CREATE PROCEDURE [dbo].[SelectAllPadecimientoPersona]
	@idpersona uniqueidentifier
AS
	select pe.IdPersona, pe.Padecimiento from PadecimientoEnfermedades pe where pe.IdPersona = @idpersona
RETURN 0
