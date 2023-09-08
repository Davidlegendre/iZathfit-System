using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PermisosModel
    {
        public Guid IdUsuario { get; set; }
        public int IdVentana { get;set; }
        public bool Crear { get; set; }
        public bool Leer { get; set; }
        public bool Actualizar { get; set; }
        public bool Eliminar { get; set; }  
    }
}
