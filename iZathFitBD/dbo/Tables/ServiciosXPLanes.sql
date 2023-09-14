CREATE TABLE [dbo].[ServiciosXPLanes]
(
	idPlan int not null,
	idServicio int not null,
	foreign key (idPlan) references Planes(IdPlanes),
	foreign key (idServicio) references Servicio(IdServicio)
)
