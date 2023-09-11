create table Rol (
IdRol int identity not null,
descripcion varchar(50) not null,
code char(4) not null unique,
primary key (IdRol)
)