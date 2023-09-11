using Configuration;
using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Rol
{
    public class RolRepository : IRolRepository
    {
        IGeneralConfiguration _config;
        public RolRepository(IGeneralConfiguration config)
        {
            _config = config;
        }

      
        
        public async Task<List<RolModel>?> GetRoles() { 
            using(var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryAsync<RolModel>("SelectAllRol", 
                    commandType: System.Data.CommandType.StoredProcedure);
                
                return result.Count() == 0 ? null : result.AsList();
            }
        }

       
    }
}
