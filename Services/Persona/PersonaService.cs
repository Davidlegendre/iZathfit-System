using Domain.Persona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Persona
{
    public class PersonaService : IPersonaService
    {
        IPersonaRepository _personarepo;
        public PersonaService(IPersonaRepository personarepo)
        {
            _personarepo = personarepo;
        }

        public async Task<Guid?> VerificarEmail(string? email)
        { 
            return await _personarepo.ConsultaPersonaByEmail(email);
        }
    }
}
