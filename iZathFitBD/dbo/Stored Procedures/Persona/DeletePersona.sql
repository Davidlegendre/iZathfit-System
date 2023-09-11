create procedure DeletePersona
	@idPersona uniqueidentifier
	as
	BEGIN
		declare @exist uniqueidentifier
		set @exist = (select u.IdPersona from Usuario u where u.IdPersona = @idPersona)
		if(@exist is not null)
			THROW 51000, 'Esta persona ya tiene un usuario, solo queda desactivar su usuario', 1;
		
		set @exist = (select c.IdPersona from Contrato c where c.IdPersona = @idPersona)
		if(@exist is not null)
			THROW 51000, 'Esta persona ya tiene un contrato, no puede eliminar', 1;
		
		delete from OcupacionXPersona where IdPersona = @idPersona
		delete from Persona where IdPersona = @idPersona
		delete from PadecimientoEnfermedades where IdPersona = @idPersona
		delete from NumEmergencias where IdPersona = @idPersona
	END