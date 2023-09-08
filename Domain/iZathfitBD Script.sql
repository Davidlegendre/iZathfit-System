use master;
go
drop database izathfitbd;
go
create database izathfitbd;
go
use izathfitbd;
go
--tables

Create table Configuracion(
IdConfiguracion int identity not null,
NombreEmpresa varchar(50) not null,
Direccion varchar(100),
Correo varchar(100),
Descripcion varchar(100),
Primary key (IdConfiguracion)
)

create table TipoRedSocial(
IdTipoRedSocial int identity not null,
descripcion varchar(50) not null,
primary key (IdTipoRedSocial)
)

create table RedesSociales(
IdRedSocial int identity not null,
IdConfiguracion int not null,
idTipoRedSocial int not null,
redsocial varchar(70) not null,
primary key (idRedSocial),
Foreign Key (IdConfiguracion) references Configuracion(IdConfiguracion),
foreign key (idTipoRedSocial) references TipoRedSocial(IdTipoRedSocial)
)

create table TipoPago(
IdtipoPago int identity not null,
descripcion varchar(50) not null,
primary key (IdtipoPago)
)

create table Rol (
IdRol int identity not null,
descripcion varchar(30) not null,
code char(4) not null unique,
primary key (IdRol)
)

create table Genero(
IdGenero int identity not null,
descripcion varchar(10) not null,
code char(1) not null,
primary key (IdGenero)
)

create table TipoIdentity(
IdTipoIdentity int identity not null,
descripcion varchar(50) not null,
abreviado varchar(10) not null,
primary key (IdTipoIdentity)
)

create table Persona(
IdPersona uniqueidentifier not null,
Nombres varchar(50) not null,
Apellidos varchar(50) not null,
Fech_Nacimiento datetime not null,
Edad as (Year(Getdate()) - Year(Fech_Nacimiento)),
Direccion varchar(100) not null,
Telefono char(9) not null,
Email varchar(50) unique,
idGenero int not null,
Identificacion varchar(20) not null unique,
idtipoIdentificacion int not null,
idRol int not null,
primary key (IdPersona),
foreign key (idGenero) references Genero(IdGenero),
foreign key (idtipoIdentificacion) references TipoIdentity(IdTipoIdentity),
foreign key (idRol) references Rol(IdRol)
)

--Separar Ocupacion de Roles y quede solo 4 roles y varia su permiso en admin
--todas las ocupaciones --info
--añadir tabla de permisos para administrador
--agregar idusuario a contrato y a otros
--Desarrollador => administra administradores, clientes, dueños
--Dueño => principal, administra administradores, clientes, tiene todo
--Administrador => tiene Permisos y administra clientes limite
--cliente => solo ve sus datos
--Roles FIjos en sistema (Dueño, Desarrollador, Cliente)
--Tabla ventanas => IdVentanas, Titulo, Codigo (MANUSU75)
--MANUSU75 => MANtenimiento de USUarios 75
--Permisos => todos menos clientes
/*
	idUsuario
	idventana
	Create
	Read
	Update
	Delete
*/

create table Ventana(
	idVentana int identity not null,
	Titulo varchar(100) not null,
	Codigo char(8) not null,
	primary key (idVentana)
)


create table Ocupacion(
	IdOcupacion int identity not null,
	descripcion varchar(100) not null,
	primary key (IdOcupacion)
)

create table OcupacionXPersona(
IdPersona uniqueidentifier not null,
IdOcupacion int not null,
foreign key (IdPersona) references Persona(IdPersona),
foreign key (IdOcupacion) references Ocupacion(IdOcupacion)
)

create table PadecimientoEnfermedades(
IdPersona uniqueidentifier not null,
Padecimiento varchar(100) not null,
foreign key (IdPersona) references Persona(IdPersona)
)

create table NumEmergencias(
IdPersona uniqueidentifier not null,
Nombres varchar(50) not null,
Apellido varchar(50) not null,
Celular varchar(10) not null,
foreign key (IdPersona) references Persona(IdPersona)
)

create table Usuario(
idUsuario uniqueidentifier not null,
IdPersona uniqueidentifier not null unique,
usuario varchar(10) not null unique,
contrasena varchar(16) not null,
IsActivo bit not null default(1),
primary key (idUsuario),
foreign key (IdPersona) references Persona(IdPersona)
)

create table Permisos(
IdUsuario uniqueidentifier not null,
IdVentana int not null,
Crear bit not null,
Leer bit not null,
Actualizar bit not null,
Eliminar bit not null
)

create table Servicio(
IdServicio int identity not null,
NombreServicio varchar(100) not null,
HorarioInicio datetime not null,
HorarioFin datetime not null,
IsGrupal bit not null default(0),
IdUsuario uniqueidentifier not null,
primary key (IdServicio),
foreign key (IdUsuario) references Usuario(idUsuario)
)

create table PlanDuracion(
IdPlanDuracion int identity not null,
descripcion varchar(10) not null,
IdUsuario uniqueidentifier not null,
MesesTiempo int  not null check (MesesTiempo >= 1 and MesesTiempo <= 12),
Primary Key (IdPlanDuracion),
foreign key (IdUsuario) references Usuario(idUsuario)
)

create table Planes(
IdPlanes int identity not null,
IdServicio int not null,
IdPlanDuracion int not null,
IdUsuario uniqueidentifier not null,
descripcion varchar(100),
Precio decimal(18,2) not null,
primary key (IdPlanes),
foreign key (IdServicio) references Servicio(IdServicio),
foreign key (IdUsuario) references Usuario(idUsuario),
foreign key (IdPlanDuracion) references PlanDuracion(IdPlanDuracion),
)

create table Promociones(
IdPromocion int identity not null,
IdPlan int not null,
IdUsuario uniqueidentifier not null,
DescuentoPercent int not null check (DescuentoPercent >= 1 and DescuentoPercent <= 100),
IsActivo bit not null default(0),
DuracionTiempo datetime not null,
descripcion varchar(100),
Primary key (IdPromocion),
foreign key (IdPlan) references Planes(IdPlanes),
foreign key (IdUsuario) references Usuario(idUsuario)
)

create table Contrato(
IdContrato uniqueidentifier not null,
IdPlan int not null,
IdPersona uniqueidentifier not null,
IdUsuario uniqueidentifier not null,
ValorTotal decimal(18,2) not null,
Descuento int default(0),
ValorOriginal decimal(18,2) not null,
FechaInicio datetime not null default(getdate() + 1),
FechaFinal datetime not null,
Fecha_contrato datetime not null default(getdate()),
NumeroContrato char(6) not null unique,
IdTipoPago int not null,
primary key (IdContrato),
foreign key (IdPlan) references Planes(IdPlanes),
foreign key (IdPersona) references Persona(IdPersona),
foreign key (IdTipoPago) references TipoPago(IdTipoPago),
foreign key (IdUsuario) references Usuario(idUsuario)
)

create table Saldos(
IdPersona uniqueidentifier not null,
IsVencido bit not null default(0),
IdPlan int not null,
ValorTotal decimal(18,2) not null,
PagoActual decimal(18,2) not null,
)
go
--procedures
--------------------------------------------------------------------------
------------------Tabla Genero--------------------------
go
	create procedure SelectAllGenero
	as
	BEGIN 
		select IdGenero, descripcion, code from Genero
	END
go
	create procedure SelectOneGenero
	@idGenero int
	as
	BEGIN
		select IdGenero, descripcion, code from Genero where IdGenero = @idGenero
	END
go
---------------------------------------------------------------
go
------------------Tipo Red Social------------------------------
go
	create procedure SelectAllTipoRedSocial
	as
	BEGIN 
		select IdTipoRedSocial, descripcion from TipoRedSocial
	END
go
	create procedure SelectOneTipoRedSocial
	@idTipoRedSocial int
	as
	BEGIN
		select IdTipoRedSocial, descripcion from TipoRedSocial where IdTipoRedSocial = @idTipoRedSocial
	END
go
	create procedure InsertTipoRedSocial
	@Tiporedsocial varchar(50)
	as
	BEGIN
		Insert into TipoRedSocial(descripcion) values(@Tiporedsocial)
	END
go
	create procedure UpdateTipoRedSocial
	@idTipoRedSocial int, @Tiporedsocial varchar(50)
	as
	BEGIN 
		update TipoRedSocial set descripcion = @Tiporedsocial
		where IdTipoRedSocial = @idTipoRedSocial
	END
go
	create procedure DeleteTipoRedSocial
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
go
----------------------------------------------------------------
--------------Login---------------------------
	create procedure IniciarSesion
	@user varchar(10), @password varchar(16)
	as
	BEGIN
		select top 1 u.IdPersona from Usuario u 
			where u.usuario = @user and u.contrasena = @password
	END
------------------------------------------------
go
create procedure VerificarActivoUsuario
@idPersona uniqueidentifier
as
BEGIN
		declare @isactive bit
		
		set @isactive = (select u.IsActivo from Usuario u where u.IdPersona = @idPersona)
		if(@isactive = 0)
			THROW 51000, 'Este usuario no se encuentra', 1;
END
go
---------------------------------------------
-------------Persona----------------
	create procedure PersonaDatos
	@ID uniqueidentifier
	as
	BEGIN
		select p.IdPersona, p.Nombres,
		p.Apellidos, p.Fech_Nacimiento, p.Edad,
		p.Direccion, p.Telefono, p.Email,
		p.Identificacion, r.descripcion as 'Rol', r.code as 'CodeRol' , g.descripcion as 'GeneroDescripcion',
		g.code as 'generoCode', ti.descripcion as 'TipoIdentityDescripcion',
		ti.abreviado as 'abreviadoTipo'
		from Persona p
		inner join Genero g on g.IdGenero = p.idGenero
		inner join TipoIdentity ti on ti.IdTipoIdentity = p.idtipoIdentificacion
		inner join Rol r on r.IdRol = p.idRol
		where p.IdPersona = @ID
	END
go
	create procedure VerificarEmailPersona
	@email varchar(25)
	as
	BEGIN
		select p.IdPersona from Persona p where p.Email = @email
	END

go
	create procedure AgregarPersona
		@Nombres varchar(50), @Apellidos varchar(50),
		@Fech_nac datetime, @Direccion varchar(100),
		@idrol int, @telefono char(9),
		@Email varchar(50), @idgenero int, 
		@Identificacion varchar(20), @idTipoIdent int
	as
	BEGIN
		declare @uid uniqueidentifier
		set @uid = newid()
		insert into Persona(IdPersona, 
		Nombres, Apellidos, Fech_Nacimiento, Direccion, 
		idRol, Telefono, Email,idGenero,
		Identificacion,idtipoIdentificacion)
		values(@uid,@Nombres,@Apellidos,@Fech_nac,@Direccion,
		@idrol, @telefono,@Email,@idgenero,@Identificacion,@idTipoIdent)
		select @uid
	END
go
	create procedure UpdatePersonaData
		@Nombres varchar(50), @Apellidos varchar(50),
		@Fech_nac datetime, @Direccion varchar(100),
		@idRol int, @telefono varchar(10),
		@Email varchar(25), @idgenero int, 
		@Identificacion varchar(20), @idTipoIdent int, @id uniqueidentifier
	as
	Begin
		update Persona set Nombres = @Nombres,
		Apellidos = @Apellidos, Fech_Nacimiento = @Fech_nac,
		Direccion = @Direccion, idRol = @idRol,
		Telefono = @telefono, Email = @Email, idGenero = @idgenero,
		Identificacion = @Identificacion, idtipoIdentificacion = @idTipoIdent
		where IdPersona = @id
	End
go
	create procedure SelectAllPersonasNormal
	as 
	BEGIN
		Select * from Persona
	END
go
	create procedure SelectAllPersonsJoin
	as 
	Begin
		select p.IdPersona, p.Nombres, p.Apellidos, p.Fech_Nacimiento,
		p.Edad, p.Direccion, r.descripcion as 'Rol', r.code as 'CodeRol', p.Telefono, p.Email, g.descripcion as 'Genero',
		g.code as 'CodeGenero', ti.descripcion as 'Identificacion', ti.abreviado as 'CodeIdentificacion'
		from Persona p
		inner join Genero g on  g.IdGenero = p.idGenero
		inner join TipoIdentity ti on ti.IdTipoIdentity = p.idtipoIdentificacion
		inner join Rol r on r.IdRol = p.idRol
	end
go
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
-----------------------------------

go
----------------------------------------
---------------Usuario---------------------
	create procedure ChangePasswordUser
	@password varchar(16), @idpersona uniqueidentifier
	AS
	BEGIN
		declare @isequals varchar(16)
		set @isequals = (select u.contrasena from Usuario u where u.IdPersona = @idpersona)
		if(@isequals = @password)
			THROW 51000, 'La contraseña es la misma', 1;
		else
		BEGIN
			update Usuario set contrasena = @password where IdPersona = @idpersona
		END
	END
go
------------------------------------------
----------------OcupacionXPersona--------------
create procedure OcupacionPorPersona
@idPersona uniqueidentifier
as 
BEGIN
	select o.IdOcupacion, o.descripcion as 'Description' from OcupacionXPersona OXP 
	inner join Ocupacion o on o.IdOcupacion = OXP.IdOcupacion
	where OXP.IdPersona = @idPersona
END
-------------------------------------
go
--------------------------------------------------------------
---------------------Ventana--------------------------------
create procedure InsertarVentana
@Titulo varchar(100),
@Codigo char(8)
as 
BEGIN
	Insert into Ventana(Titulo, Codigo) values(@Titulo, @Codigo)
END
go

create procedure EliminarVentana
@idVentana int, @idUsuario uniqueidentifier
as
BEGIN
	declare @exist int
	set @exist = (select count(p.IdUsuario) from Permisos p where p.IdUsuario = @idUsuario)
	if(@exist <> 0)
		THROW 51000, 'El usuario ya tiene esta ventana en sus permisos', 1;

	delete from Ventana where idVentana = @idVentana

END
go

create procedure ActualizarVentana
@Titulo varchar(100),
@Codigo char(8), @idVentana int
as
BEGIN
	update Ventana set Titulo = @Titulo, Codigo = @Codigo 
	where idVentana = @idVentana
END
go

create procedure SelectAllVentana
as
BEGIN
	select * from Ventana
END
go

create procedure SelectVentanaOne
@idVentana int
as
BEGIN
	select * from Ventana where idVentana = @idVentana
END
--------------------------------------------------------------
go
---------------------------------------------------------------
---------------------------------------------------------------
--Initial Data
insert into Genero(descripcion, code) values('Masculino','M'),('Femenino','F')
insert into TipoRedSocial(descripcion) values('Whatsapp'),('Facebook'),('Instagram'),('Pagina Web')
insert into Rol(descripcion, code) values
('Dueño', 'DUNO'),('Desarrollador','DEVP'),('Cliente','CLNT'),('Administrador', 'ADMN')
insert into Ocupacion(descripcion) values ('Recepcionista'),('Instructor/Entrenador'),('Limpieza'),('Tec. Computacion e Informatica')
insert into TipoIdentity(descripcion, abreviado) values
('Documento Nacional de Indentidad', 'DNI'),
('Carnet de Extranjeria', 'CE'),
('Pasaporte','PS')

declare @uid uniqueidentifier
set @uid = newid()
insert into Persona(IdPersona, Nombres, 
Apellidos,Direccion,Email,Fech_Nacimiento,Identificacion,Telefono,idRol, 
idGenero, idtipoIdentificacion) values
(@uid,'Fulano', 'De Tal', 'Urb. X','email@email.com','1990-01-06',
'80880808','080808808',3,1,1)
insert into OcupacionXPersona(IdPersona, IdOcupacion) values(@uid, 4)
declare @uidusuario uniqueidentifier
set @uidusuario = newid()
insert into Usuario(idUsuario,IdPersona, usuario, contrasena,IsActivo) values
(@uidusuario,@uid, 'fulano', 'fulano123', 1)
go
--Tests