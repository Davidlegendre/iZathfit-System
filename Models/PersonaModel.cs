using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PersonaModel
    {

        public Guid IdPersona { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public DateTime Fech_Nacimiento { get; set; }
        public int Edad { get; set; }
        public string? Direccion { get; set; }
        public int IdRol { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public int IdGenero { get; set; }
        public string? NumeroEmergencia1 { get; set; }
        public string? NumeroEmergencia2 { get; set; }
        public string? Identificacion { get; set; }
        public int IdTipoIdentity { get; set; }

        public string? TipoIdentAbreviado { get; set; }

        public string? Genero { get; set; }
        public string? TipoIdentificacion { get; set; }
        public string? Rol { get; set; }
        public string? CodeRol { get; set; }
        public int IdOcupacion { get; set; }
        public string? Ocupacion { get; set; }

        public string? GetCompleteName => Nombres + " " + Apellidos;
        public string? GetEdad => "Edad: " + Edad;
        public string? GetDireccion => "Dir: " + Direccion;
        public string? GetTelefono => "Tlf: " + Telefono;
        public string? GetIdentificacion => TipoIdentAbreviado + ": " + Identificacion;
        public string? GetNumerosEmergencias => "Emergencias: " + NumeroEmergencia1 + ", " + NumeroEmergencia2;
        public string? getNumEmergencia => "Tlf Emergencia: " + NumeroEmergencia1;
        public string? GetFechaNacimiento => "Fecha de Nacimiento: " + Fech_Nacimiento.ToLongDateString();


    }
}
