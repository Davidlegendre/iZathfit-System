CREATE TABLE [dbo].[Rutina]
(
	IdRutina uniqueidentifier NOT NULL PRIMARY KEY,
	MontoPago decimal(18,2) not null,
	IdTipoPago int not null,
	FechaPagoRutina datetime not null default(Getdate()),
	foreign key (IdTipoPago) references TipoPago(IdtipoPago)
)
