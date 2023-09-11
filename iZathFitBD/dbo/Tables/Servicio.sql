create table Servicio(
IdServicio int identity not null,
NombreServicio varchar(100) not null,
HorarioInicio datetime not null,
HorarioFin datetime not null,
IsGrupal bit not null default(0),
IdUsuario uniqueidentifier not null,
primary key (IdServicio),
foreign key (IdUsuario) references Usuario(idUsuario)
)