CREATE PROCEDURE [dbo].[SelectAllUsuario]
AS
BEGIN
	select u.idUsuario, u.IdPersona, u.usuario, u.contrasena, u.IsActivo,
	CONCAT(p.Nombres, ' ' , p.Apellidos) as 'Persona', r.descripcion as 'Rol', r.code as 'codeRol'
	from Usuario u
	inner join Persona p on p.IdPersona = u.IdPersona
	inner join Rol r on r.IdRol = p.idRol
END
