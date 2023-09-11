/*
Procedimientos Almacenados de Tabla TipoRedSocial
Cree la base de datos primero
*/
go
	create procedure SelectAllTipoRedSocial
	as
	BEGIN 
		select IdTipoRedSocial, descripcion from TipoRedSocial
	END
go
	create procedure SelectOneTipoRedSocial
	@idTipoRedSocial int
	as
	BEGIN
		select IdTipoRedSocial, descripcion from TipoRedSocial where IdTipoRedSocial = @idTipoRedSocial
	END
go
	create procedure InsertTipoRedSocial
	@Tiporedsocial varchar(100)
	as
	BEGIN
		Insert into TipoRedSocial(descripcion) values(@Tiporedsocial)
	END
go
	create procedure UpdateTipoRedSocial
	@idTipoRedSocial int, @Tiporedsocial varchar(100)
	as
	BEGIN 
		update TipoRedSocial set descripcion = @Tiporedsocial
		where IdTipoRedSocial = @idTipoRedSocial
	END
go
	create procedure DeleteTipoRedSocial
	@idTipoRedSocial int, @isdelete bit out
	as
	BEGIN
		declare @count int
		set @count = (select count(rs.IdTipoRedSocial) from RedesSociales rs where rs.IdTipoRedSocial = @idTipoRedSocial)
		if(@count > 0)
			return 0
		else
		BEGIN
			delete from TipoRedSocial where IdTipoRedSocial = @idTipoRedSocial
			return 1
		END
	END
go