create procedure VerificarEmailPersona
	@email varchar(255)
	as
	BEGIN
		select p.IdPersona from Persona p where p.Email = @email
	END