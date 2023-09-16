create table Promociones(
IdPromocion int identity not null,
IdPlan int not null,
IdUsuario uniqueidentifier not null,
DescuentoPercent int not null check (DescuentoPercent >= 1 and DescuentoPercent <= 100),
IsActivo as case when Convert(Date,DuracionTiempo) < Convert(date, Getdate()) then 0 else 1 end,
DuracionTiempo datetime not null,
descripcion varchar(100),
Primary key (IdPromocion),
foreign key (IdPlan) references Planes(IdPlanes),
foreign key (IdUsuario) references Usuario(idUsuario)
)