create procedure InsertTipoRedSocial
	@Tiporedsocial varchar(100)
	as
	BEGIN
		Insert into TipoRedSocial(descripcion) values(@Tiporedsocial)
	END