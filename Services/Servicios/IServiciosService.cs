﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Servicios
{
    public interface IServiciosService
    {
        public Task<List<Models.ServicioModel>?> GetServicios();
        public Task<bool> InsertServicios(ServicioModel servicio);
        public Task<bool> UpdateServicio(ServicioModel servicio);
        public Task<bool> DeleteServicio(int idservicio);
    }
}
