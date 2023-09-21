CREATE PROCEDURE [dbo].[InsertTipoPago]
	@descripcion varchar(100)
AS
	insert into TipoPago(descripcion) values(@descripcion)
RETURN 0
