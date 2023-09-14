﻿using CommunityToolkit.Mvvm.ComponentModel;
using Models;
using Services.Servicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iZathfit.ViewModels.Pages.Negocio
{
    public partial class ServiciosPageVM : ObservableObject
    {
        IServiciosService? _servicio;
        public ServiciosPageVM()
        {
            _servicio = App.GetService<IServiciosService>();
        }

        [ObservableProperty]
        ObservableCollection<ServicioModel>? _servicios = new ObservableCollection<ServicioModel>();

        public async Task CargarDatos() {
            if (_servicio == null) return;
            var serv = await _servicio.GetServicios();
            Servicios = serv != null? new ObservableCollection<ServicioModel>(serv) : null;

        }

    }
}
