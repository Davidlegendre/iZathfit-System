CREATE PROCEDURE [dbo].[CountPersonas]
AS
	select count(p.IdPersona) from Persona p
RETURN 0
