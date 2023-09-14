CREATE PROCEDURE [dbo].[InsertTipoIdentificacion]
	@abreviado varchar(20), @descripcion varchar(50)
AS
	insert into TipoIdentity(descripcion, abreviado) values(@descripcion, @abreviado)
	declare @ultimo int
	set @ultimo = (select top 1 ti.IdTipoIdentity from TipoIdentity ti order by ti.IdTipoIdentity desc)
	exec dbo.SelectOneTipoIdentificacion @ultimo
RETURN 0
