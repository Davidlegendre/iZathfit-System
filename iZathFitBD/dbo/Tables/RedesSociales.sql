create table RedesSociales(
IdRedSocial int identity not null,
IdConfiguracion int not null,
idTipoRedSocial int not null,
redsocial varchar(255) not null,
primary key (idRedSocial),
Foreign Key (IdConfiguracion) references Configuracion(IdConfiguracion),
foreign key (idTipoRedSocial) references TipoRedSocial(IdTipoRedSocial)
)