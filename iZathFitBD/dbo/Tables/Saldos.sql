create table Saldos(
IdPersona uniqueidentifier not null,
IsVencido bit not null default(0),
IdPlan int not null,
ValorTotal decimal(18,2) not null,
PagoActual decimal(18,2) not null,
)