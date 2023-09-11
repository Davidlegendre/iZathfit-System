create table OcupacionXPersona(
IdPersona uniqueidentifier not null,
IdOcupacion int not null,
foreign key (IdPersona) references Persona(IdPersona),
foreign key (IdOcupacion) references Ocupacion(IdOcupacion)
)