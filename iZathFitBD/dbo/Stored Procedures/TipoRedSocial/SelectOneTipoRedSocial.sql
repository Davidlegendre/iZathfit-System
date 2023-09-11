create procedure SelectOneTipoRedSocial
	@idTipoRedSocial int
	as
	BEGIN
		select IdTipoRedSocial, descripcion from TipoRedSocial where IdTipoRedSocial = @idTipoRedSocial
	END