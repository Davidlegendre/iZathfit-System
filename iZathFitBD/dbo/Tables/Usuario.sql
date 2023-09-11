create table Usuario(
idUsuario uniqueidentifier not null,
IdPersona uniqueidentifier not null unique,
usuario varchar(10) not null unique,
contrasena varchar(16) not null,
IsActivo bit not null default(1),
primary key (idUsuario),
foreign key (IdPersona) references Persona(IdPersona)
)