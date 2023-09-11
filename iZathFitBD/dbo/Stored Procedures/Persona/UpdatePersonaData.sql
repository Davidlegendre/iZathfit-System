create procedure UpdatePersonaData
		@Nombres varchar(50), @Apellidos varchar(50),
		@Fech_nac datetime, @Direccion varchar(100),
		@idRol int, @telefono varchar(10),
		@Email varchar(255), @idgenero int, 
		@Identificacion varchar(20), @idTipoIdent int, @id uniqueidentifier
	as
	Begin
		update Persona set Nombres = @Nombres,
		Apellidos = @Apellidos, Fech_Nacimiento = @Fech_nac,
		Direccion = @Direccion, idRol = @idRol,
		Telefono = @telefono, Email = @Email, idGenero = @idgenero,
		Identificacion = @Identificacion, idtipoIdentificacion = @idTipoIdent
		where IdPersona = @id
		
		exec izathfitbd.dbo.SelectOnePersona @id
		
	End