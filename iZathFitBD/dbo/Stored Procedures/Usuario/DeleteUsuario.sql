CREATE PROCEDURE [dbo].[DeleteUsuario]
	@idusuario uniqueidentifier
AS
	declare @exist uniqueidentifier
	set @exist = (select top 1 c.IdUsuario from Contrato c where c.IdUsuario = @idusuario)
	if(@exist is not null)
		THROW 51000, 'Este usuario ya esta en uso, imposible eliminar', 1;

	set @exist = (select top 1 pd.IdUsuario from PlanDuracion pd where pd.IdUsuario = @idusuario)
	if(@exist is not null)
		THROW 51000, 'Este usuario ya esta en uso, imposible eliminar', 1;

	set @exist = (select top 1  p.IdUsuario from Planes p where p.IdUsuario = @idusuario)
	if(@exist is not null)
		THROW 51000, 'Este usuario ya esta en uso, imposible eliminar', 1;

	set @exist = (select top 1 p.IdUsuario from Promociones p where p.IdUsuario = @idusuario)
	if(@exist is not null)
		THROW 51000, 'Este usuario ya esta en uso, imposible eliminar', 1;
	
	set @exist = (select top 1 p.IdUsuario from Servicio p where p.IdUsuario = @idusuario)
	if(@exist is not null)
		THROW 51000, 'Este usuario ya esta en uso, imposible eliminar', 1;

	exec dbo.SelectOneUsuario @idusuario

	delete from Usuario where idUsuario = @idusuario

RETURN 0
