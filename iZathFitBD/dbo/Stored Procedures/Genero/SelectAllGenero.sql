create procedure SelectAllGenero
	as
	BEGIN 
		select IdGenero, descripcion, code from Genero
	END