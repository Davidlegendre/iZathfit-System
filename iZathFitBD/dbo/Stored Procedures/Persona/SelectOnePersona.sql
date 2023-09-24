create procedure SelectOnePersona
	@id uniqueidentifier
	as
	BEGIN
	Select p.Nombres, p.Apellidos, p.Direccion, p.Edad, p.Email, 
			p.Fech_Nacimiento, p.Identificacion, p.Telefono, p.IdPersona, g.descripcion as 'Genero',
			ti.descripcion as 'TipoIdentificacion', ti.abreviado as 'TipoIdentAbreviado',
			r.descripcion as 'Rol', r.IdRol, g.IdGenero, ti.IdTipoIdentity,
			o.descripcion as 'Ocupacion', o.IdOcupacion, p.NumeroEmergencia1, p.NumeroEmergencia2
			from Persona p
			inner join Genero g on g.IdGenero = p.idGenero
			inner join TipoIdentity ti on ti.IdTipoIdentity = p.idtipoIdentificacion
			inner join Rol r on r.IdRol = p.idRol
			inner join Ocupacion o on o.IdOcupacion = p.idOcupacion
			where p.IdPersona = @id
END