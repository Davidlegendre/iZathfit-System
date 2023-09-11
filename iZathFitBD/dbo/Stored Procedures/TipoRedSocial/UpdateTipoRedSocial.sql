create procedure UpdateTipoRedSocial
	@idTipoRedSocial int, @Tiporedsocial varchar(100)
	as
	BEGIN 
		update TipoRedSocial set descripcion = @Tiporedsocial
		where IdTipoRedSocial = @idTipoRedSocial
	END