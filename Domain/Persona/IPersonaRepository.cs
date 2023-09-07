using Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Persona
{
    public interface IPersonaRepository
    {
        public Task<UsuarioSistema?> GetPersonaData(Guid? ID);
    }
}
