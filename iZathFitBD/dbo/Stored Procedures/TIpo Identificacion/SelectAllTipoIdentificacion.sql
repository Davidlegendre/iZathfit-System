create procedure SelectAllTipoIdentificacion
	as
	BEGIN
		select IdTipoIdentity, abreviado, descripcion from TipoIdentity
	END