using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Persona
{
    public interface IPersonaService
    {
        public  Task<Guid?> VerificarEmail(string? email);
    }
}
