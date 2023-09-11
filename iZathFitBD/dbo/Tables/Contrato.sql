create table Contrato(
IdContrato uniqueidentifier not null,
IdPlan int not null,
IdPersona uniqueidentifier not null,
IdUsuario uniqueidentifier not null,
ValorTotal decimal(18,2) not null,
Descuento int default(0),
ValorOriginal decimal(18,2) not null,
FechaInicio datetime not null default(getdate() + 1),
FechaFinal datetime not null,
Fecha_contrato datetime not null default(getdate()),
NumeroContrato char(6) not null unique,
IdTipoPago int not null,
primary key (IdContrato),
foreign key (IdPlan) references Planes(IdPlanes),
foreign key (IdPersona) references Persona(IdPersona),
foreign key (IdTipoPago) references TipoPago(IdTipoPago),
foreign key (IdUsuario) references Usuario(idUsuario)
)