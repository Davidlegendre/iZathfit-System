using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RolXPersona
{
    public interface IRolXPersonaRepository
    {
        public Task<List<Rol>?> GetRols(Guid? IdPersona);
    }
}
