CREATE PROCEDURE [dbo].[SearchPersonaByNameLastNameIdentify]
	@Texto varchar(max)
AS
	Select Top 10 p.Nombres, p.Apellidos, p.Direccion, p.Edad, p.Email, 
		p.Fech_Nacimiento, p.Identificacion, p.Telefono, p.IdPersona, g.descripcion as 'Genero',
		ti.descripcion as 'TipoIdentificacion', r.descripcion as 'Rol', r.IdRol,
		r.code as 'CodeRol', ti.abreviado as 'TipoIdentAbreviado',
		g.IdGenero, ti.IdTipoIdentity, o.descripcion as 'Ocupacion', o.IdOcupacion, p.NumeroEmergencia1,
		p.NumeroEmergencia2
		from Persona p
		inner join Genero g on g.IdGenero = p.idGenero
		inner join TipoIdentity ti on ti.IdTipoIdentity = p.idtipoIdentificacion
		inner join Rol r on r.IdRol = p.idRol
		inner join Ocupacion o on o.IdOcupacion = p.idOcupacion
		where CONCAT(p.Nombres, ' ', p.Apellidos) like '%' + @Texto + '%'
		or p.Identificacion like '%' + @Texto + '%'
RETURN 0
