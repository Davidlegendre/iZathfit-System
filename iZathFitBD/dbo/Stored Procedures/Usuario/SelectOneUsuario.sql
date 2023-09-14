CREATE PROCEDURE [dbo].[SelectOneUsuario]
	@idusuario uniqueidentifier
AS
	select u.idUsuario, u.IdPersona, u.usuario, u.contrasena, u.IsActivo,
	CONCAT(p.Nombres, ' ' , p.Apellidos) as 'Persona', r.descripcion as 'Rol'
	from Usuario u
	inner join Persona p on p.IdPersona = u.IdPersona
	inner join Rol r on r.IdRol = p.idRol
	where u.idUsuario = @idusuario
RETURN 0
