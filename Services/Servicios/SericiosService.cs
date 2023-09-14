using Domain.Servicios;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Servicios
{
    public class ServiciosService : IServiciosService
    {
        IServiciosRepositoryI _repo;
        public ServiciosService(IServiciosRepositoryI repo)
        {
            _repo = repo;
        }
        public async Task<bool> DeleteServicio(int idservicio)
        {
            return await _repo.DeleteServicio(idservicio);
        }

        public async Task<List<ServicioModel>?> GetServicios()
        {
            
            return await _repo.GetServicios();
        }

        public async Task<bool> InsertServicios(ServicioModel servicio)
        {
            return await _repo.InsertServicios(servicio);
        }

        public async Task<bool> UpdateServicio(ServicioModel servicio)
        {
            return await _repo.UpdateServicio(servicio);
        }
    }
}
