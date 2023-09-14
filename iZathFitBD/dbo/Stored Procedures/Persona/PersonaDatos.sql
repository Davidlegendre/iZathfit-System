create procedure PersonaDatos
	@ID uniqueidentifier
	as
	BEGIN
		select p.IdPersona, p.Nombres,
		p.Apellidos, p.Fech_Nacimiento, p.Edad,
		p.Direccion, p.Telefono, p.Email,
		p.Identificacion, r.descripcion as 'Rol', r.code as 'CodeRol' , g.descripcion as 'GeneroDescripcion',
		g.code as 'generoCode', ti.descripcion as 'TipoIdentityDescripcion',
		ti.abreviado as 'abreviadoTipo', u.idUsuario, u.contrasena,
		o.descripcion as 'Ocupacion', p.NumeroEmergencia1, p.NumeroEmergencia2
		from Persona p
		inner join Genero g on g.IdGenero = p.idGenero
		inner join TipoIdentity ti on ti.IdTipoIdentity = p.idtipoIdentificacion
		inner join Rol r on r.IdRol = p.idRol
		inner join Usuario u on u.IdPersona = p.IdPersona
		inner join Ocupacion o on o.IdOcupacion = p.idOcupacion
		where p.IdPersona = @ID
	END