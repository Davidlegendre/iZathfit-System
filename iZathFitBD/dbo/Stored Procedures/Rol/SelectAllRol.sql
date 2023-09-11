create procedure SelectAllRol
	as 
	BEGIN
		select IdRol, descripcion, code from Rol
	END