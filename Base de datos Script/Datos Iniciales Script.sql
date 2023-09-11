---------------------------------------------------------------
--Initial Data
/*
Datos Iniciales
Cree la base de datos primero
*/
go
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
	'80880808','080808808',2,1,1)
	insert into OcupacionXPersona(IdPersona, IdOcupacion) values(@uid, 4)
	declare @uidusuario uniqueidentifier
	set @uidusuario = newid()
	insert into Usuario(idUsuario,IdPersona, usuario, contrasena,IsActivo) values
	(@uidusuario,@uid, 'fulano', 'fulano123', 1)
go