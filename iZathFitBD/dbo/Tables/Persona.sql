create table Persona(
IdPersona uniqueidentifier not null,
Nombres varchar(50) not null,
Apellidos varchar(50) not null,
Fech_Nacimiento datetime not null,
Edad as (Year(Getdate()) - Year(Fech_Nacimiento)),
Direccion varchar(100) not null,
Telefono char(9) not null,
Email varchar(255),
idGenero int not null,
Identificacion varchar(20) not null unique,
idtipoIdentificacion int not null,
idRol int not null,
primary key (IdPersona),
foreign key (idGenero) references Genero(IdGenero),
foreign key (idtipoIdentificacion) references TipoIdentity(IdTipoIdentity),
foreign key (idRol) references Rol(IdRol)
)