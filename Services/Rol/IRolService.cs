using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Rol
{
    public interface IRolService
    {
        public Task<List<RolModel>?> GetRoles();
    }
}
