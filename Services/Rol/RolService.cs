using Configuration;
using Configuration.GlobalHelpers;
using Domain.Rol;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Rol
{
    public class RolService : IRolService
    {
        IRolRepository _repo;
        IGlobalHelpers _helpers;
        public RolService(IRolRepository repo, IGlobalHelpers helpers)
        {
            _repo = repo;
            _helpers = helpers;
        }
      
        public async Task<List<RolModel>?> GetRoles() { 
            var result = await _repo.GetRoles();
            
            return _helpers.FiltrarRoles(result);
        }
    }
}
