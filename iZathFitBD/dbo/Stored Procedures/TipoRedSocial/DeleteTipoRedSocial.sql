CREATE procedure DeleteTipoRedSocial
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