create table Planes(
IdPlanes int identity not null,
IdServicio int not null,
IdPlanDuracion int not null,
IdUsuario uniqueidentifier not null,
descripcion varchar(100),
Precio decimal(18,2) not null,
primary key (IdPlanes),
foreign key (IdServicio) references Servicio(IdServicio),
foreign key (IdUsuario) references Usuario(idUsuario),
foreign key (IdPlanDuracion) references PlanDuracion(IdPlanDuracion),
)