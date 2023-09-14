using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS
{
    public class PadecimientosEnfermedadesDTO
    {
        public Guid Idpersona { get; set; }
        public PersonaModel Persona { get; set; }   
        public List<string> Enfermedades { get; set; }   
    }
}
