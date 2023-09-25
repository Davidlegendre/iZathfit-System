﻿---------------------------------------------------------------
--Initial Data
/*
Datos Iniciales
Cree la base de datos primero
*/

GO

	use iZathFitBD
	insert into dbo.Genero(descripcion, code) values('Masculino','M'),('Femenino','F')
	insert into dbo.Rol(descripcion, code) values('Desarrollador','DEVP'),
	('Dueño', 'DUNO'),('Cliente','CLNT'),('Administrador', 'ADMN')
	insert into dbo.Ocupacion(descripcion) values ('Otros'),('Ventas'),('Instructor/Entrenador'),('Limpieza'),('Tec. Computacion e Informatica')
	insert into dbo.TipoIdentity(descripcion, abreviado) values
	('Documento Nacional de Indentidad', 'DNI'),
	('Carnet de Extranjeria', 'CE'),
	('Pasaporte','PS')
	insert into dbo.TipoPago(descripcion) 
	values('Efectivo'),('Tarjeta'), ('Plin'), ('Yape')


	declare @uid uniqueidentifier
	set @uid = newid()
	insert into dbo.Persona(IdPersona, Nombres, 
	Apellidos,Direccion,Email,Fech_Nacimiento,Identificacion,Telefono,idRol, 
	idGenero, idtipoIdentificacion, idOcupacion, NumeroEmergencia1) values
	(@uid,'nombres', 'apellidos', 'direccion','email','año-mes-dia',
	'identificacion','numero',1,1,1,5, 'numeroemergencia')
	declare @uidusuario uniqueidentifier
	set @uidusuario = newid()
	insert into dbo.Usuario(idUsuario,IdPersona, usuario, contrasena,IsActivo) values
	(@uidusuario,@uid, 'usuario', 'contraseña', 1)

GO