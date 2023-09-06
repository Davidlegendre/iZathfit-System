using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Login
{
    public class UserModel
    {
        public string Nombres { get; set; }
        public Guid Id { get; set; }
        public string Apellidos { get; set; }
        public string NombreCompleto => Nombres + " " + Apellidos;
        public List<RolModel>? Roles { get; set; }    
        
    }
}
