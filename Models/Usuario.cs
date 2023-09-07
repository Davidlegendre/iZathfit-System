using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Usuario
    {
        public Guid IdPersona { get; set; }
        public string usuario { get; set; }
        public string contrasena { get; set; }
    }
}
