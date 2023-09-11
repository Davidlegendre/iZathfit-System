create procedure VerificarActivoUsuario
@idPersona uniqueidentifier
as
BEGIN
		declare @isactive bit
		
		set @isactive = (select u.IsActivo from Usuario u where u.IdPersona = @idPersona)
		if(@isactive = 0)
			THROW 51000, 'Este usuario no se encuentra', 1;
END