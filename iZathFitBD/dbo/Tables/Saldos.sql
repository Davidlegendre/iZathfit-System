create table Saldos(
IdSaldos Uniqueidentifier not null,
IdPersona uniqueidentifier not null,
IsVencido bit not null default(0),
IdContrato uniqueidentifier not null,
ValorTotal decimal(18,2) not null default(0),
TotalPagado decimal(18,2) not null check (TotalPagado <= ValorTotal) default(0),
TotalFaltante  as ValorTotal - TotalPagado,
Primary key (IdSaldos),
foreign key (IdPersona) references Persona(IdPersona),
foreign key (IdContrato) references Contrato(IdContrato)
)