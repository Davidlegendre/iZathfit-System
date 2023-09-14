using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicios
{
    public interface IServiciosRepositoryI
    {
        public Task<List<Models.ServicioModel>?> GetServicios();
        public Task<bool> InsertServicios(ServicioModel servicio);
        public Task<bool> UpdateServicio(ServicioModel servicio);
        public Task<bool> DeleteServicio(int idservicio);
    }
}
