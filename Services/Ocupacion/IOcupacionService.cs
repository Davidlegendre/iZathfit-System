using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Ocupacion
{
    public interface IOcupacionService
    {
        public Task<List<Models.Ocupacion>?> GetOcupaciones();
        public Task<Models.Ocupacion?> InsertOcupacion(Models.Ocupacion ocupacion);
        public Task<Models.Ocupacion?> UpdateOcupacion(Models.Ocupacion ocupacion);
        public Task<int> DeleteOcupacion(int idocupacion);
    }
}
