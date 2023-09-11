create table Permisos(
IdUsuario uniqueidentifier not null,
IdVentana int not null,
Crear bit not null,
Leer bit not null,
Actualizar bit not null,
Eliminar bit not null,
foreign key (IdUsuario) references Usuario(IdUsuario),
foreign key (IdVentana) references Ventana(IdVentana)
)