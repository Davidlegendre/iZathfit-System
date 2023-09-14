CREATE PROCEDURE [dbo].[UpdateUsuario]
	@idPersona uniqueidentifier, @usuario varchar(10),
	@contrasena varchar(16), @idusuario uniqueidentifier, @isactivo bit
AS
	declare @id uniqueidentifier
	declare @user varchar(10)
	declare @rol char(4)
	set @rol = (select r.code from Persona p 
	inner join Rol r on r.IdRol = p.idRol
	where p.IdPersona = @idPersona
	)
	if(@rol = 'CLNT')
		THROW 51000, 'Un Cliente no puede tener su usuario', 1;
	set @id = (select u.IdPersona from Usuario u where u.IdPersona = @idPersona)
	set @user = (select u.usuario from Usuario u where u.usuario = @usuario)
	if(@id is not null and @id <> @idPersona)
		THROW 51000, 'Esta persona ya tiene un usuario', 1;

	if(@user is not null and @user <> @usuario)
		THROW 51000, 'Este usuario no esta disponible para agregar', 1;

	update Usuario set IdPersona = @idPersona, usuario = @usuario, contrasena = @contrasena, IsActivo = @isactivo
	where idUsuario = @idusuario

	exec dbo.SelectOneUsuario @idusuario

RETURN 0
