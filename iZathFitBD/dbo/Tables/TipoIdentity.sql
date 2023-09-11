create table TipoIdentity(
IdTipoIdentity int identity not null,
descripcion varchar(50) not null,
abreviado varchar(20) not null,
primary key (IdTipoIdentity)
)