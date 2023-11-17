CREATE PROCEDURE [dbo].[InsertUsuario]
	@idPersona uniqueidentifier, @usuario varchar(10),
	@contrasena varchar(max)
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
	if(@id is not null)
		THROW 51000, 'Esta persona ya tiene un usuario', 1;

	if(@user is not null)
		THROW 51000, 'Este usuario no esta disponible para agregar', 1;

	set @id = NEWID()
	insert into Usuario(idUsuario, IdPersona, usuario, contrasena)
	values(@id, @idPersona, @usuario, @contrasena)

	exec dbo.SelectOneUsuario @id

RETURN 0
