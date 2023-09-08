using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS
{
    public class PersonaDTO
    {
        /* p.Nombres, p.Apellidos, p.Fech_Nacimiento,
		p.Edad, p.Direccion, p.Ocupacion, p.Telefono, p.Email, g.descripcion as 'Genero',
		g.code as 'CodeGenero', ti.descripcion as 'Identificacion', ti.abreviado as 'CodeIdentificacion*/
        public Guid IdPersona { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public DateTime Fech_Nacimiento { get; set; }
        public int Edad { get; set; }
        public string? Direccion { get; set; }
        public string? Rol { get; set; }
        public string? CodeRol { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Genero { get; set; }
        public string? CodeGenero { get; set; }
        public string? Identificacion { get; set; }
        public string? CodeIdentificacion { get; set; }
    }
}
