create procedure AgregarPersona
		@Nombres varchar(50), @Apellidos varchar(50),
		@Fech_nac datetime, @Direccion varchar(100),
		@idrol int, @telefono varchar(9),
		@Email varchar(255), @idgenero int, 
		@Identificacion varchar(20), @idTipoIdent int,
		@numemergencia1 varchar(10), @numemergencia2 varchar(10),
		@idocupacion int
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
		Identificacion,idtipoIdentificacion, idOcupacion, NumeroEmergencia1, NumeroEmergencia2)
		values(@uid,@Nombres,@Apellidos,@Fech_nac,@Direccion,
		@idrol, @telefono,@Email,@idgenero,@Identificacion,@idTipoIdent, @idocupacion, @numemergencia1, @numemergencia2)

		exec izathfitbd.dbo.SelectOnePersona @uid
	END