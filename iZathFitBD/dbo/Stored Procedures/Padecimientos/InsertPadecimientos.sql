CREATE PROCEDURE [dbo].[InsertPadecimientos]
	@idpersona uniqueidentifier,
	@padecimiento varchar(100)
AS
	insert into PadecimientoEnfermedades(Padecimiento, IdPersona) values(@padecimiento, @idpersona)
RETURN 0
