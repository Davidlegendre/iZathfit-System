using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OcupacionXPersona
{
    public interface IOcupacionXPersonaRepository
    {
        public Task<List<Ocupacion>?> OcupacionPorPersona(Guid? idPersona);
    }
}
