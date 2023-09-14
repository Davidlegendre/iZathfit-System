CREATE PROCEDURE [dbo].[AgregarOcupacion]
	@descripcion varchar(100)
AS
	insert into Ocupacion(descripcion) values(@descripcion)
	declare @idultimo int
	set @idultimo = (select top 1 IdOcupacion from Ocupacion order by IdOcupacion desc)
	exec SelectOneOcupacion @idultimo
RETURN 0
