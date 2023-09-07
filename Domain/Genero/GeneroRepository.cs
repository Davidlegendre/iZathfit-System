using Configuration;
using Dapper;
using Models;
using System.Data.SqlClient;

namespace Domain.Genero
{
    public class GeneroRepository : IGeneroRepository
    {
        IGeneralConfiguration _config;
        
        public GeneroRepository(IGeneralConfiguration config)
        {
            _config = config;
        }

        public async Task<List<GeneroModel>?> GetGeneros() {
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var generos = await con.QueryAsync<GeneroModel>("SelectAllGenero", 
                    commandType: System.Data.CommandType.StoredProcedure);
                return generos.AsList();
            }
        }
    }
}
