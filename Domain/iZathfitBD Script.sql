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
LogoEmpresa varbinary,
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
IdRedSocial int not null,
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
primary key (IdGenero)
)

create table TipoIdentity(
IdTipoIdentity int identity not null,
descripcion varchar(30) not null,
abreviado varchar(10) not null,
primary key (IdTipoIdentity)
)

create table Persona(
IdPersona uniqueidentifier not null default newid(),
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
IdPersona uniqueidentifier not null,
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
MesesTiempo int  not null default(1) check (MesesTiempo >= 1 and MesesTiempo <= 12),
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
DuracionTiempo datetime not null default(Getdate()),
descripcion varchar(100),
Primary key (IdPromocion),
foreign key (IdPlan) references Planes(IdPlanes)
)

create table Contrato(
IdContrato uniqueidentifier not null default(newid()),
Codigo varchar(6) not null, --automatico o lo registran
IdPlan int not null,
IdPersona uniqueidentifier not null,
ValorTotal decimal(18,2) not null,
Descuento int default(0),
ValorOriginal decimal(18,2) not null,
FechaInicio datetime not null default(getdate() + 1),
FechaFinal datetime not null,
Fecha_contrato datetime not null default(getdate()),
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


--precio
--contrato => plan (precio), descuento, promocion, valortotal

--usuarioXRol (usuario con varios roles)
--usuario puede tener administrador, cliente, instructor (administrador)
go
--procedures
go
--Initial Data
go
--Tests