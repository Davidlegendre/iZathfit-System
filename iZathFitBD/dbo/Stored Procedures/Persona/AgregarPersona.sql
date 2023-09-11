create procedure AgregarPersona
		@Nombres varchar(50), @Apellidos varchar(50),
		@Fech_nac datetime, @Direccion varchar(100),
		@idrol int, @telefono char(9),
		@Email varchar(255), @idgenero int, 
		@Identificacion varchar(20), @idTipoIdent int
	as
	BEGIN
		declare @uid uniqueidentifier
		set @uid = newid()
		declare @exist int
		set @exist = (select count(p.Identificacion) from Persona p where p.Identificacion = @Identificacion)
		if(@exist <> 0)
			THROW 51000, 'Esta indentificacion ya existe en la base de datos', 1;

		insert into Persona(IdPersona, 
		Nombres, Apellidos, Fech_Nacimiento, Direccion, 
		idRol, Telefono, Email,idGenero,
		Identificacion,idtipoIdentificacion)
		values(@uid,@Nombres,@Apellidos,@Fech_nac,@Direccion,
		@idrol, @telefono,@Email,@idgenero,@Identificacion,@idTipoIdent)

		exec izathfitbd.dbo.SelectOnePersona @uid
	END