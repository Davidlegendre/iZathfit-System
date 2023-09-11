/*
Procedimientos Almacenados de Tabla Persona
Cree la base de datos primero
*/
go
	create procedure PersonaDatos
	@ID uniqueidentifier
	as
	BEGIN
		select p.IdPersona, p.Nombres,
		p.Apellidos, p.Fech_Nacimiento, p.Edad,
		p.Direccion, p.Telefono, p.Email,
		p.Identificacion, r.descripcion as 'Rol', r.code as 'CodeRol' , g.descripcion as 'GeneroDescripcion',
		g.code as 'generoCode', ti.descripcion as 'TipoIdentityDescripcion',
		ti.abreviado as 'abreviadoTipo'
		from Persona p
		inner join Genero g on g.IdGenero = p.idGenero
		inner join TipoIdentity ti on ti.IdTipoIdentity = p.idtipoIdentificacion
		inner join Rol r on r.IdRol = p.idRol
		where p.IdPersona = @ID
	END
go
	create procedure VerificarEmailPersona
	@email varchar(255)
	as
	BEGIN
		select p.IdPersona from Persona p where p.Email = @email
	END

go
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
		insert into Persona(IdPersona, 
		Nombres, Apellidos, Fech_Nacimiento, Direccion, 
		idRol, Telefono, Email,idGenero,
		Identificacion,idtipoIdentificacion)
		values(@uid,@Nombres,@Apellidos,@Fech_nac,@Direccion,
		@idrol, @telefono,@Email,@idgenero,@Identificacion,@idTipoIdent)

		exec izathfitbd.dbo.SelectOnePersona @uid
	END
go
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
go
	create procedure SelectAllPersonas
	as 
	BEGIN
		Select p.Nombres, p.Apellidos, p.Direccion, p.Edad, p.Email, 
		p.Fech_Nacimiento, p.Identificacion, p.Telefono, p.IdPersona, g.descripcion as 'Genero',
		ti.descripcion as 'TipoIdentificacion', r.descripcion as 'Rol', r.IdRol, g.IdGenero, ti.IdTipoIdentity
		from Persona p
		inner join Genero g on g.IdGenero = p.idGenero
		inner join TipoIdentity ti on ti.IdTipoIdentity = p.idtipoIdentificacion
		inner join Rol r on r.IdRol = p.idRol
	END
go
	create procedure SelectOnePersona
	@id uniqueidentifier
	as
	BEGIN
	Select p.Nombres, p.Apellidos, p.Direccion, p.Edad, p.Email, 
			p.Fech_Nacimiento, p.Identificacion, p.Telefono, p.IdPersona, g.descripcion as 'Genero',
			ti.descripcion as 'TipoIdentificacion', r.descripcion as 'Rol', r.IdRol, g.IdGenero, ti.IdTipoIdentity
			from Persona p
			inner join Genero g on g.IdGenero = p.idGenero
			inner join TipoIdentity ti on ti.IdTipoIdentity = p.idtipoIdentificacion
			inner join Rol r on r.IdRol = p.idRol
			where p.IdPersona = @id
END
go
	create procedure DeletePersona
	@idPersona uniqueidentifier
	as
	BEGIN
		declare @exist uniqueidentifier
		set @exist = (select u.IdPersona from Usuario u where u.IdPersona = @idPersona)
		if(@exist is not null)
			THROW 51000, 'Esta persona ya tiene un usuario, solo queda desactivar su usuario', 1;
		
		set @exist = (select c.IdPersona from Contrato c where c.IdPersona = @idPersona)
		if(@exist is not null)
			THROW 51000, 'Esta persona ya tiene un contrato, no puede eliminar', 1;
		
		delete from OcupacionXPersona where IdPersona = @idPersona
		delete from Persona where IdPersona = @idPersona
		delete from PadecimientoEnfermedades where IdPersona = @idPersona
		delete from NumEmergencias where IdPersona = @idPersona
	END
go