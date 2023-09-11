--tables

Create table Configuracion(
IdConfiguracion int identity not null,
NombreEmpresa varchar(120) not null,
Direccion varchar(100),
Correo varchar(255),
Descripcion varchar(100),
Primary key (IdConfiguracion)
)