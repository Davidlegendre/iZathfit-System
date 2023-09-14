create procedure UpdatePersonaData
		@Nombres varchar(50), @Apellidos varchar(50),
		@Fech_nac datetime, @Direccion varchar(100),
		@idRol int, @telefono varchar(9),
		@Email varchar(255), @idgenero int, 
		@idocupacion int, @numemergencia1 varchar(10), @numemergencia2 varchar(10),
		@Identificacion varchar(20), @idTipoIdent int, @id uniqueidentifier
	as
	--si ya tiene un usuario y quiere cambiarlo a cliente no puede
	Begin
		declare @existUsuario uniqueidentifier
		set @existUsuario = (select top 1 u.idUsuario from Usuario u where u.IdPersona = @id)
		if(@existUsuario is not null)
		BEGIN
			declare @rol char(4)
			set @rol = (select r.code from Rol r where r.IdRol = @idRol)
			if(@rol = 'CLNT')
				THROW 51000, 'Un Cliente no puede tener usuario', 1;
		END

		update Persona set Nombres = @Nombres,
		Apellidos = @Apellidos, Fech_Nacimiento = @Fech_nac,
		Direccion = @Direccion, idRol = @idRol,
		Telefono = @telefono, Email = @Email, idGenero = @idgenero,
		idOcupacion = @idocupacion,
		NumeroEmergencia1 = @numemergencia1,
		NumeroEmergencia2 = @numemergencia2,
		Identificacion = @Identificacion, idtipoIdentificacion = @idTipoIdent
		where IdPersona = @id
		
		exec izathfitbd.dbo.SelectOnePersona @id
		
	End