CREATE PROCEDURE [dbo].[InsertarPlanes]
	@ServiciosType ServiciosType readonly,
	@idplanduracion int,
	@idusuario uniqueidentifier,
	@precio decimal(18,2),
	@descripcion varchar(100)
AS
	declare @idultimo int
		begin try
			insert into Planes(IdPlanDuracion, IdUsuario, descripcion, Precio)
			values(@idplanduracion, @idusuario, @descripcion, @precio)

			set @idultimo = (select top 1 p.IdPlanes from Planes p order by p.IdPlanes desc)
			insert into ServiciosXPLanes(idPlan, IdServicio) select @idultimo, IdServicio from @ServiciosType
		end try	
		begin catch
			THROW 51000, 'Ocurrio un erro, revise procedimiento dbo.InsertarPlanes', 1;
		end catch
RETURN 0
