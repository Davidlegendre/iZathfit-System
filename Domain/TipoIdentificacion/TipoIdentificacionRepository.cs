using Configuration;
using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TipoIdentificacion
{
    public class TipoIdentificacionRepository : ITipoIdentificacionRepository
    {
        IGeneralConfiguration _config;
        public TipoIdentificacionRepository(IGeneralConfiguration config)
        {
            _config = config;
        }

        public async Task<List<TipoIdentificacionModel>?> GetTipoIdentificacion()
        {
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryAsync<TipoIdentificacionModel>("SelectAllTipoIdentificacion",
                    commandType: System.Data.CommandType.StoredProcedure);
                return result.Count() == 0 ? null : result.AsList();
            }
        }
    }
}
