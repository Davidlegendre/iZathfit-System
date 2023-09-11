
--use master;
--GO

--drop database izathfitbd;
--GO

--create database izathfitbd;
--GO

--use izathfitbd;
--GO

---------------------------------------------------------------
--Initial Data
/*
Datos Iniciales
Cree la base de datos primero
*/

GO

	use iZathFitBD
	insert into dbo.Genero(descripcion, code) values('Masculino','M'),('Femenino','F')
	insert into dbo.TipoRedSocial(descripcion) values('Whatsapp'),('Facebook'),('Instagram'),('Pagina Web')
	insert into dbo.Rol(descripcion, code) values
	('Dueño', 'DUNO'),('Desarrollador','DEVP'),('Cliente','CLNT'),('Administrador', 'ADMN')
	insert into dbo.Ocupacion(descripcion) values ('Recepcionista'),('Instructor/Entrenador'),('Limpieza'),('Tec. Computacion e Informatica')
	insert into dbo.TipoIdentity(descripcion, abreviado) values
	('Documento Nacional de Indentidad', 'DNI'),
	('Carnet de Extranjeria', 'CE'),
	('Pasaporte','PS')

	declare @uid uniqueidentifier
	set @uid = newid()
	insert into dbo.Persona(IdPersona, Nombres, 
	Apellidos,Direccion,Email,Fech_Nacimiento,Identificacion,Telefono,idRol, 
	idGenero, idtipoIdentificacion) values
	(@uid,'David Fernando', 'Legendre Albites', 'Urb. La Perla','dlegendre74@gmail.com','1996-06-09',
	'49001564','914847720',2,1,1)
	insert into dbo.OcupacionXPersona(IdPersona, IdOcupacion) values(@uid, 4)
	declare @uidusuario uniqueidentifier
	set @uidusuario = newid()
	insert into dbo.Usuario(idUsuario,IdPersona, usuario, contrasena,IsActivo) values
	(@uidusuario,@uid, 'david', 'david123', 1)

GO

/*
Procedimientos Almacenados de Tabla Genero
Cree la base de datos primero
*/

GO


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

GO

/*
Procedimientos Almacenados Login
Cree la base de datos primero
*/

GO

/*
Procedimientos Almacenados de Tabla OcupacionXPersona
Cree la base de datos primero
*/

GO

/*
Procedimientos Almacenados de Tabla Persona
Cree la base de datos primero
*/

GO

/*
Procedimientos Almacenados de Tabla Rol
Cree la base de datos primero
*/

GO

/*
Procedimientos Almacenados de Tabla Tipo Identificacion
Cree la base de datos primero
*/

GO

/*
Procedimientos Almacenados de Tabla TipoRedSocial
Cree la base de datos primero
*/

GO

/*
Procedimientos Almacenados de Tabla Usuario
Cree la base de datos primero
*/

GO

/*
Procedimientos Almacenados de Tabla Ventana
Cree la base de datos primero
*/


GO
