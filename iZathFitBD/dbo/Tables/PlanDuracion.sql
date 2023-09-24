create table PlanDuracion(
IdPlanDuracion int identity not null,
descripcion varchar(100),
IdUsuario uniqueidentifier not null,
MesesTiempo int  not null,
Primary Key (IdPlanDuracion),
foreign key (IdUsuario) references Usuario(idUsuario)
)