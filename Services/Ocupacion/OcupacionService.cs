using Domain.Ocupaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Ocupacion
{
    public class OcupacionService :IOcupacionService
    {
        IOcupacionRepository _repo;
        public OcupacionService(IOcupacionRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Models.Ocupacion>?> GetOcupaciones()
        {
            return await _repo.GetOcupaciones();
        }

        public async Task<Models.Ocupacion?> InsertOcupacion(Models.Ocupacion ocupacion)
        {
            return await _repo.InsertOcupacion(ocupacion);
        }

        public async Task<Models.Ocupacion?> UpdateOcupacion(Models.Ocupacion ocupacion)
        {
            return await _repo.UpdateOcupacion(ocupacion);
        }
        public async Task<int> DeleteOcupacion(int idocupacion)
        {
            return await _repo.DeleteOcupacion(idocupacion);
        }
    }
}
