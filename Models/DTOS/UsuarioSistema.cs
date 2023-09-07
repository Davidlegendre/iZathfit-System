using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS
{
    public class UsuarioSistema
    {

        public Guid IdPersona { get; }
       
        public string? Nombres { get; set;}
        public string? Apellidos { get; set; }
        public DateTime Fech_Nacimiento { get; set; }
        public int Edad { get; }
        public string NombreCompleto => Nombres + " " + Apellidos;
        public string? Direccion { get; set; }
        public string? Ocupacion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Identificacion { get; set;}
        public string? GeneroDescripcion { get; set; }
        public string? generoCode { get; set; }

        public string?  TipoIdentityDescripcion { get; set; }
        public string? abreviadoTipo { get; set; }
        public List<Rol>? Roles { get; set; }
        
    }
}
