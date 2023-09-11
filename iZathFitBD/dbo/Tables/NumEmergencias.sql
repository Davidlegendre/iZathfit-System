create table NumEmergencias(
IdPersona uniqueidentifier not null,
Nombres varchar(50) not null,
Apellido varchar(50) not null,
Celular varchar(10) not null,
foreign key (IdPersona) references Persona(IdPersona)
)