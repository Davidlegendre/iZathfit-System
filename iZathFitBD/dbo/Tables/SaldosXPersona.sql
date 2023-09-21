CREATE TABLE [dbo].[SaldosXPersona]
(
	IdPositionRow int not null,
	IdPersona uniqueidentifier not null,
	IdContrato uniqueidentifier not null,
	IdSaldo uniqueidentifier not null,
	TotalPagadoActual decimal(18,2) not null,
	IdTipoPago int not null,
	FechaPago datetime not null default(getdate()),
	foreign key (IdPersona) references Persona(IdPersona),
	foreign key (IdContrato) references Contrato(IdContrato),
	foreign key (IdSaldo) references Saldos(IdSaldos),
	foreign key (IdTipoPago) references TipoPago(IdTipoPago)
)
