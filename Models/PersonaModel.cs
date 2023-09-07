using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PersonaModel
    {
        public Guid IdPersona { get;  }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime Fech_Nacimiento { get; set; }
        public int Edad { get; }
        public string Direccion { get; set; }
        public string Ocupacion { get; set; }
        public string Telefono { get; set; }
        public string? Email { get; set; }
        public int idGenero { get; set; }
        public string Identificacion { get; set; }
        public int idtipoIdentificacion { get; set; }


    }
}
