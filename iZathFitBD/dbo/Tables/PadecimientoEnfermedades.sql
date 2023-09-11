create table PadecimientoEnfermedades(
IdPersona uniqueidentifier not null,
Padecimiento varchar(100) not null,
foreign key (IdPersona) references Persona(IdPersona)
)