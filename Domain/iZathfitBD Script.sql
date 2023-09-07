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
Ocupacion varchar(100) not null,
Telefono varchar(10) not null,
Email varchar(25),
idGenero int not null,
Identificacion varchar(20) not null unique,
idtipoIdentificacion int not null,
primary key (IdPersona),
foreign key (idGenero) references Genero(IdGenero),
foreign key (idtipoIdentificacion) references TipoIdentity(IdTipoIdentity)
)

create table RolXPersona(
IdPersona uniqueidentifier not null,
idRol int not null,
foreign key (IdPersona) references Persona(IdPersona),
foreign key (idRol) references Rol(IdRol)
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
IdPersona uniqueidentifier not null unique,
usuario varchar(10) not null unique,
contrasena varchar(16) not null,
foreign key (IdPersona) references Persona(IdPersona)
)

create table Servicio(
IdServicio int identity not null,
NombreServicio varchar(100) not null,
HorarioInicio datetime not null,
HorarioFin datetime not null,
IsGrupal bit not null default(0),
primary key (IdServicio)
)

create table PlanDuracion(
IdPlanDuracion int identity not null,
descripcion varchar(10) not null,
MesesTiempo int  not null check (MesesTiempo >= 1 and MesesTiempo <= 12),
Primary Key (IdPlanDuracion)
)

create table Planes(
IdPlanes int identity not null,
IdServicio int not null,
IdPlanDuracion int not null,
descripcion varchar(100),
Precio decimal(18,2) not null,
primary key (IdPlanes),
foreign key (IdServicio) references Servicio(IdServicio),
foreign key (IdPlanDuracion) references PlanDuracion(IdPlanDuracion)
)



create table Promociones(
IdPromocion int identity not null,
IdPlan int not null,
DescuentoPercent int not null check (DescuentoPercent >= 1 and DescuentoPercent <= 100),
IsActivo bit not null default(0),
DuracionTiempo datetime not null,
descripcion varchar(100),
Primary key (IdPromocion),
foreign key (IdPlan) references Planes(IdPlanes)
)

create table Contrato(
IdContrato uniqueidentifier not null,
IdPlan int not null,
IdPersona uniqueidentifier not null,
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
foreign key (IdTipoPago) references TipoPago(IdTipoPago)
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
---------------------------------------------
-------------Persona----------------
	create procedure PersonaDatos
	@ID uniqueidentifier
	as
	BEGIN
		select p.IdPersona, p.Nombres,
		p.Apellidos, p.Fech_Nacimiento, p.Edad,
		p.Direccion, p.Ocupacion, p.Telefono, p.Email,
		p.Identificacion, g.descripcion as 'GeneroDescripcion',
		g.code as 'generoCode', ti.descripcion as 'TipoIdentityDescripcion',
		ti.abreviado as 'abreviadoTipo'
		from Persona p
		inner join Genero g on g.IdGenero = p.idGenero
		inner join TipoIdentity ti on ti.IdTipoIdentity = p.idtipoIdentificacion
		where p.IdPersona = @ID
	END
-----------------------------------
go
------------------------------------------
----------------RolXPersona--------------
create procedure RolesPorPersona
@idPersona uniqueidentifier
as 
BEGIN
	select r.IdRol, r.descripcion as 'Description', r.code as 'Code' from RolXPersona rxp 
	inner join Rol r on r.IdRol = rxp.idRol
	where rxp.IdPersona = @idPersona
END
-------------------------------------
go
---------------------------------------------------------------
---------------------------------------------------------------
--Initial Data
insert into Genero(descripcion, code) values('Masculino','M'),('Femenino','F')
insert into TipoRedSocial(descripcion) values('Whatsapp'),('Facebook'),('Instagram'),('Pagina Web')
insert into Rol(descripcion, code) values
('Dueño', 'DUNO'),('Administrador','ADMN'),('Recepcionista','RCPS'),('Instructor/Entrenador','INEN'),
('Limpieza', 'LMPZ'),('Cliente','CLNT'),('Desarrollador','DEVP')
insert into TipoIdentity(descripcion, abreviado) values
('Documento Nacional de Indentidad', 'DNI'),
('Carnet de Extranjeria', 'CE'),
('Pasaporte','PS')

declare @uid uniqueidentifier
set @uid = newid()
insert into Persona(IdPersona, Nombres, 
Apellidos,Direccion,Email,Fech_Nacimiento,Identificacion,Telefono,Ocupacion, 
idGenero, idtipoIdentificacion) values
(@uid,'David Fernando', 'Legendre Albites', 'Urb. La Perla','dlegendre74@gmail.com','1996-09-06',
'49001564','914847720','Desarrollador de Software', 1,1)
insert into RolXPersona(IdPersona, idRol) values(@uid, 7),(@uid, 2)
insert into Usuario(IdPersona, usuario, contrasena) values
(@uid, 'Devdavid', 'david.dev2023')
go
--Tests