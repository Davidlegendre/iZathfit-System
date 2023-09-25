CREATE PROCEDURE [dbo].[InsertarClienteTodoConContratoYPago]
	@Nombres varchar(50),
	@Apellidos varchar(50),
	@Fech_Nacimiento datetime,
	@Direccion varchar(100) ,
	@Telefono varchar(9) ,
	@Email varchar(255),
	@NumeroEmergencia1 varchar(10),
	@NumeroEmergencia2 varchar(10),
	@idGenero int,
	@Identificacion varchar(20),
	@idtipoIdentificacion int ,
	@idRol int,
	@idOcupacion int,

	@IdPlan int,
	@IdUsuario uniqueidentifier,
	@ValorTotal decimal(18,2),
	@Descuento int,
	@ValorOriginal decimal(18,2),
	@FechaFinal datetime,
	@FechaFinalReal datetime,
	@NumeroContrato char(6),
	@IdTipoPago int,

	@TotalPagadoActual decimal(18,2),

	@Padecimientos PadecimientoType readonly

AS
set nocount on;
declare @trancount int
set @trancount = @@TRANCOUNT

		Begin Try
			if(@trancount = 0)
				begin Transaction
			else
				save transaction ups_IACliente

			declare @idPersona uniqueidentifier
			declare @idcontrato uniqueidentifier
			exec dbo.AgregarPersona @Nombres, @Apellidos, @Fech_Nacimiento, @Direccion, @idRol, @Telefono, @Email, @idGenero, @Identificacion,@idtipoIdentificacion, @NumeroEmergencia1, @NumeroEmergencia2, @idOcupacion, @idpersona = @idPersona output
			exec dbo.InsertContratos @IdPlan, @idPersona, @IdUsuario, @ValorTotal, @Descuento, @ValorOriginal, @FechaFinal, @FechaFinalReal, @NumeroContrato, @IdTipoPago, @idcontrato = @idcontrato output
			
			
			insert into PadecimientoEnfermedades(IdPersona, Padecimiento) select @idPersona, padecimiento from @Padecimientos
			
			exec insertSaldoXPersona @idPersona, @idcontrato, @TotalPagadoActual, @IdTipoPago

			Ibexit:
				if(@trancount = 0)
					commit;
		End Try
		Begin Catch
			declare @error int, @message varchar(4000), @xstate int, @severity int, @state int

			select @error = ERROR_NUMBER(), @message =  ERROR_MESSAGE() , @xstate = XACT_STATE(), @severity = ERROR_SEVERITY(), @state = ERROR_STATE()
			if @xstate = -1
				rollback
			if @xstate = 1 and @trancount=0
				rollback;
			if @xstate = 1 and @trancount > 0
				rollback transaction ups_IACliente
			raiserror(@message, @severity, @state)
		End Catch
RETURN 0
